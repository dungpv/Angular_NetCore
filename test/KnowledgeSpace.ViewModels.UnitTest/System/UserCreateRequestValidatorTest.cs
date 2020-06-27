﻿using KnowledgeSpace.ViewModels.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace KnowledgeSpace.ViewModels.UnitTest.System
{
    public class UserCreateRequestValidatorTest
    {
        private UserCreateRequestValidator validator;
        private UserCreateRequest request;
        public UserCreateRequestValidatorTest()
        {
            request = new UserCreateRequest()
            {
                Dob = DateTime.Now.ToString(),
                Email = "vietdungst@gmail.com",
                FirstName = "Test",
                LastName = "test",
                Password = "Admin@123",
                PhoneNumber = "123456789",
                UserName = "admin",
            };
            validator = new UserCreateRequestValidator();
        }
        [Fact]
        public void Should_Valid_Result_When_Valid_Request()
        {
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Error_Result_When_Miss_UserName(string userName)
        {
            request.UserName = userName;
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Error_Result_When_Miss_Email(string email)
        {
            request.Email = email;
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Error_Result_When_Miss_LastName(string data)
        {
            request.LastName = data;
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Error_Result_When_Miss_FirstName(string data)
        {
            request.FirstName = data;
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Error_Result_When_Miss_PhoneNumber(string data)
        {
            request.PhoneNumber = data;
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
        }
        [Theory]
        [InlineData("sdasfaf")]
        [InlineData("1234567")]
        [InlineData("Admin123")]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Error_Result_When_Miss_Password_Not_Match(string data)
        {
            request.Password = data;
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
        }

    }
}
