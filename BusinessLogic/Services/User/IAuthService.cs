using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ModelsRegister;
using System.Security.Claims;
using BusinessLogic.DTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog;
using Shared.DTO;
using Shared.Auth.DTO;
using Domain.Interface.DataAcces;
using BusinessLogic.Interface;

namespace BusinessLogic.Services.User
{
    public class IAuthService: IIAuthService
    {
        private readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _IUsuario;
        private readonly IUtilsRepository _bUtils;

        public IAuthService(IConfiguration configuration, IMapper mapper, IUsuarioRepository usuario, 
            IUsuarioRepository repository,IUtilsRepository utils)
        {
            this._configuration = configuration;
            this._mapper = mapper;
            this._IUsuario = usuario;
            this._bUtils = utils;        
        }
        private string HashPassword(string password, Usuario usuario)
        {
            return _passwordHasher.HashPassword(usuario, password);
        }
        private bool VerifyPassword(string hashedPassword, string providedPassword, Usuario empresa)
        {
            var result = _passwordHasher.VerifyHashedPassword(empresa, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
        private UserToken BuildToken(TokenInfo userInfo)
        {
            string userI = userInfo.idUser.ToString();

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.UniqueName,userI),
            new Claim(ClaimTypes.Name, userInfo.name),
            new Claim(ClaimTypes.Email, userInfo.email),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
             };
            var claimsList = new List<Claim>(claims);
            if (!string.IsNullOrEmpty(userInfo.rolAdmin))
            {
                claimsList.Add(new Claim(ClaimTypes.Role, userInfo.rolAdmin));
            }
            claims = claimsList.ToArray();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["llavejwt"] ?? throw new ArgumentNullException("Error no existe llaveJWT")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(15);
            JwtSecurityToken token = new JwtSecurityToken(
                 issuer: null,
                 audience: null,
                 claims: claims,
                 expires: expiration,
                 signingCredentials: creds);
            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        public async Task<ResultAPI<object>> RegisterUser(UserRegister user)
        {
            ResultAPI<object> result = new ResultAPI<object>(true) ;
            try
            {
                bool ExistEmail = await _IUsuario.ExistsUsuario(user.Login);
                if(ExistEmail)
                {
                    result.message = "¿Ya tienes una cuenta de BiciLink?\n " +
                                   "Si tiene una cuenta, podemos ayudarle a encontrarla e iniciar sesión.\n" +
                                   " Si no la tiene, puede crear una cuenta nueva.\n";                    
                    result.code = StatusHttpResponse.Conflict;
                    return result;
                }
                
                var dbUsuario = _mapper.Map<Usuario>(user);
                //long idUser = _bUtils.getSequence("Usuario");
                string password = HashPassword(user.Password, dbUsuario);
                dbUsuario.Password = password;
                dbUsuario.FlagActivo = true;
                dbUsuario.RolId = 1;
                await _IUsuario.AddAsync(dbUsuario);
                result.Ok(StatusHttpResponse.OK);


            } catch (Exception ex) {
                result.message = "Se producto un error al registrar el usuario notificar a soporte";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex,"Error al registrar usuario");
            }
            return result ;
        }

        public async Task<ResultAPI<UserToken?>> ValidateUser(UserInfo userDTO)
        {
            ResultAPI<UserToken?> result = new ResultAPI<UserToken?>(true);
            try
            {
                Usuario? user = await _IUsuario.GetUsuarioAsync(userDTO.Login);

                if (user != null && (!string.IsNullOrEmpty(userDTO.Password)))
                {
                    if (VerifyPassword(user.Password, userDTO.Password, user))
                    {
                        TokenInfo tokenInfo = new TokenInfo()
                        {
                            email = user.Email,
                            idUser = user.IdUser,
                            name = user.Name,                           
                        };
                        switch (user.RolId )
                        {
                            case 1:
                                tokenInfo.rolAdmin = "Admin";
                                break;
                            case 2:
                                tokenInfo.rolAdmin = "User";
                                break;
                            case 3:
                                tokenInfo.rolAdmin = "Tecnico";
                                break;
                            case 4:
                                tokenInfo.rolAdmin = "Support";
                            break ; 
                            default:
                                tokenInfo.rolAdmin = "User";
                                break;
                        }
                                                                     
                        UserToken userToken = BuildToken(tokenInfo);
                        result.result = userToken;
                        result.Ok(StatusHttpResponse.OK);
                        return result;

                    }

                }

                result.code = StatusHttpResponse.BadRequest;
                result.message = "Credenciales invalidas";




            }
            catch (Exception ex)
            {
                result.message = "Se producto un error al validar el usuario notificar a soporte";
                result.error = true;
                result.code = StatusHttpResponse.InternalServerError;
                Log.Error(ex.Message);
            }
            return result;
        }

    }
}
