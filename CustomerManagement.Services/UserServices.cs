using AutoMapper;
using CustomerManagement.DTO;
using CustomerManagement.IRepositories;
using CustomerManagement.IServices;
using CustomerManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Services
{
    public class UserServices : IUserServices
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UserServices(IMapper _mapper, IUserRepository _userRepository)
        {
            mapper = _mapper;
            userRepository = _userRepository;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            try
            {
                var user = mapper.Map<User>(createUserDTO);
                
                user.CreatedDate = DateTimeOffset.Now;
                user.UpdatedDate = DateTimeOffset.Now;
                
                var newUser = await userRepository.AddUserAsync(user);

                var mappedUser = new UserDTO(
                        newUser.UserName,
                        newUser.Phone,
                        newUser.Email,
                        newUser.Address,
                        //newUser.Gender.GenderName,
                        //newUser.Role.RoleName,
                        newUser.CreatedDate
                    );
                return mappedUser;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var oldUser = await userRepository.FindByIdAsync(id);
                if (oldUser != null)
                {
                    var isDeleted = await userRepository.DeleteAsync(oldUser);
                    return isDeleted;
                }
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await userRepository.GetAllAsync();
                var l = users.Select(x => new UserDTO(
                        x.UserName,
                        x.Phone,
                        x.Email,
                        x.Address,
                        //x.Gender.GenderName,
                        //x.Role.RoleName,
                        x.CreatedDate
                ));
                return l;
                /*
                var mappedUsers= mapper.Map<IEnumerable<UserDTO>>(users);
                return mappedUsers;
                */
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(id);
                if(user!= null) {
                    /*
                    var mappedUser = mapper.Map<UserDTO>(user);
                    */
                    var mappedUser = new UserDTO(
                        user.UserName,
                        user.Phone,
                        user.Email,
                        user.Address,
                        //user.Gender.GenderName,
                        //user.Role.RoleName,
                        user.CreatedDate
                        );
                    return mappedUser;
                }
                throw new InvalidDataException($"Invalid ID");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<UserDTO> UpdateUserAsync(int id, UpdateUserDTO updateUserDTO)
        {
            try
            {
                var oldUser = await userRepository.FindByIdAsync(id);
                var myRequest = mapper.Map<User>(updateUserDTO);
                if (oldUser != null)
                {
                    myRequest.UpdatedDate = DateTimeOffset.Now;

                    var oldUserDTO = mapper.Map<UpdateUserDTO>(oldUser);
                    var myRequestDTO = mapper.Map<UpdateUserDTO>(myRequest);

                    var updateRequestDTO = mapper.Map(myRequestDTO, oldUserDTO);
                    var updateRequest = mapper.Map<User>(updateRequestDTO);

                    updateRequest.CreatedDate = oldUser.CreatedDate;
                    updateRequest.CreatedBy = oldUser.CreatedBy;

                    var updatedUser = await userRepository.UpdateAsync(updateRequest);
                    var mappedUser = new UserDTO(
                        updatedUser.UserName,
                        updatedUser.Phone,
                        updatedUser.Email,
                        updatedUser.Address,
                       // updatedUser.Gender.GenderName,
                       // updatedUser.Role.RoleName,
                        updatedUser.CreatedDate
                    );
                    return mappedUser;
                }
                else
                {
                    throw new InvalidOperationException($"User with ID {id} not found.");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
