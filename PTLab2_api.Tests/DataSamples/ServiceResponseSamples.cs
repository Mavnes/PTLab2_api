using PTLab2_api.Data.DTO;
using PTLab2_api.Data.Models;
using PTLab2_api.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTLab2_api.Tests
{
    internal class ServiceResponseSamples
    {
        private DataSamples dataSamples = new DataSamples();

        // Register

        public ServiceResponse<UserDto> RegisterSuccess(UserDto userDto) 
        {
            ServiceResponse<UserDto> response = new();

            response.Data = userDto;
            response.Message = "New user created.";
            response.Success = true;

            return response;
        }

        public ServiceResponse<UserDto> RegisterEmailAlreadyExists()
        {
            ServiceResponse<UserDto> response = new();

            response.Message = "Registration was not successful.";
            response.ErrorMessages = new List<string> { "User with this email already exists." };
            response.Success = false;
            response.Data = null;

            return response;
        }

        public ServiceResponse<UserDto> RegisterWrongSaleId()
        {
            ServiceResponse<UserDto> response = new();

            response.Message = "User was not received.";
            response.ErrorMessages = new List<string> { "Wrong sale id." };
            response.Success = false;
            response.Data = null;

            return response;
        }

        // Login

        public ServiceResponse<UserDto> LoginSuccess(UserDto userDto)
        {
            ServiceResponse<UserDto> response = new();

            response.Message = "Successful login.";
            response.Success = true;
            response.Data = userDto;

            return response;
        }

        public ServiceResponse<UserDto> LoginUserNotFound()
        {
            ServiceResponse<UserDto> response = new();

            response.Message = "Login was not successful.";
            response.ErrorMessages = new List<string> { "User with this email does not exist." };
            response.Success = false;
            response.Data = null;

            return response;
        }

        public ServiceResponse<UserDto> LoginWrongPassword()
        {
            ServiceResponse<UserDto> response = new();

            response.Message = "Login was not successful.";
            response.ErrorMessages = new List<string> { "Passwords does not match." };
            response.Success = false;
            response.Data = null;

            return response;
        }

        // Product
        public ServiceResponse<Product> ProductGetSuccess(Product product)
        {
            ServiceResponse<Product> response = new();

            response.Data = product;
            response.Message = "Product was successfully received.";
            response.Success = true;

            return response;
        }

        public ServiceResponse<Product> ProductGetNotFound()
        {
            ServiceResponse<Product> response = new();

            response.Message = "Product was not received.";
            response.ErrorMessages = new List<string> { "Product was not found." };
            response.Success = false;
            response.Data = null;

            return response;
        }

        public ServiceResponse<List<Product>> ProductGetAllSuccess(List<Product> products)
        {
            ServiceResponse<List<Product>> response = new();

            response.Data = products;
            response.Message = "The list of products was successfully received.";
            response.Success = true;

            return response;
        }

        public ServiceResponse<List<Product>> ProductGetAllNotFound()
        {
            ServiceResponse<List<Product>> response = new();

            response.Message = "The list of products was not received.";
            response.ErrorMessages = new List<string> { "Products were not found." };
            response.Success = false;
            response.Data = null;

            return response;
        }

        // Purchase

        public ServiceResponse<List<PurchaseDto>> PurchaseGetByUserSuccess()
        {
            ServiceResponse<List<PurchaseDto>> response = new();

            response.Data = dataSamples.UserPurchases;
            response.Message = "Purchases of user were successfully received.";
            response.Success = true;

            return response;
        }

        public ServiceResponse<List<PurchaseDto>> PurchaseGetByUserNotFound()
        {
            ServiceResponse<List<PurchaseDto>> response = new();

            response.ErrorMessages = new List<string> { "Wrong user id." };
            response.Success = false;
            response.Message = "Purchases of user were not received.";
            response.Data = null;

            return response;
        }

        public ServiceResponse<PurchaseDto> PurchaseMakePurchaseSuccess()
        {
            ServiceResponse<PurchaseDto> response = new();

            response.Data = dataSamples.PurchaseDto;
            response.Message = "Purchase was successfully made.";
            response.Success = true;

            return response;
        }

        public ServiceResponse<PurchaseDto> PurchaseMakePurchaseUserNotFound()
        {
            ServiceResponse<PurchaseDto> response = new();

            response.Success = false;
            response.ErrorMessages = new List<string> { "User is null." };
            response.Message = "Purchase was not made.";
            response.Data = null;

            return response;
        }

        public ServiceResponse<PurchaseDto> PurchaseMakePurchaseProductNotFound()
        {
            ServiceResponse<PurchaseDto> response = new();

            response.ErrorMessages = new List<string> { "Product was not found." };
            response.Success = false;
            response.Message = "Purchase was not made.";
            response.Data = null;

            return response;
        }
        public ServiceResponse<PurchaseDto> PurchaseMakePurchaseSuccessWithSale()
        {
            ServiceResponse<PurchaseDto> response = new();

            response.Data = dataSamples.PurchaseDtoSale;
            response.Message = "Purchase was successfully made.";
            response.Success = true;

            return response;
        }

        // User

        public ServiceResponse<UserDto> UserGetByIdSuccess()
        {
            ServiceResponse<UserDto> response = new();

            response.Data = dataSamples.UserDto;
            response.Message = "User was successfully received.";
            response.Success = true;

            return response;
        }

        public ServiceResponse<UserDto> UserGetByIdUserNotFound()
        {
            ServiceResponse<UserDto> response = new();

            response.Message = "User was not received.";
            response.ErrorMessages = new List<string> { "Wrong user id." };
            response.Success = false;
            response.Data = null;

            return response;
        }

        public ServiceResponse<UserDto> UserGetByIdSaleNotFound()
        {
            ServiceResponse<UserDto> response = new();

            response.Message = "User was not received.";
            response.ErrorMessages = new List<string> { "Wrong sale id." };
            response.Success = false;
            response.Data = null;

            return response;
        }

        public ServiceResponse<UserDto> UserUpdateSaleSuccess()
        {
            ServiceResponse<UserDto> response = new();

            response.Data = dataSamples.UserDto;
            response.Message = "Sale was successfully updated.";
            response.Success = true;

            return response;
        }

        public ServiceResponse<UserDto> UserUpdateSaleSuccessSale()
        {
            ServiceResponse<UserDto> response = new();

            response.Data = dataSamples.UserDtoSale;
            response.Message = "Sale was successfully updated.";
            response.Success = true;

            return response;
        }

        public ServiceResponse<UserDto> UserUpdateSaleUserNotFound()
        {
            ServiceResponse<UserDto> response = new();

            response.Message = "Sale was not updated.";
            response.ErrorMessages = new List<string> { "User is null." };
            response.Success = false;
            response.Data = null;

            return response;
        }

        public ServiceResponse<UserDto> UserUpdateSaleNotUpdated()
        {
            ServiceResponse<UserDto> response = new();

            response.Message = "Sale was not updated.";
            response.ErrorMessages = null;
            response.Success = false;
            response.Data = null;

            return response;
        }

        public ServiceResponse<UserDto> UserUpdateSaleSuccessForSaleUpdate()
        {
            ServiceResponse<UserDto> response = new();

            response.Data = dataSamples.UserDtoForSaleUpdate;
            response.Message = "Sale was successfully updated.";
            response.Success = true;

            return response;
        }
    }
}
