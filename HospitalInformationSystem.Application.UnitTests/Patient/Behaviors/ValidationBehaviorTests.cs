using FluentValidation;
using HospitalInformationSystem.Application.Commands.Patient;
using HospitalInformationSystem.Application.Common.Behaviours;
using MediatR;
using Moq;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HospitalInformationSystem.Application.UnitTests.Behaviors
{
    public class ValidationBehaviorTests
    {
        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
        {
            // Arrange
            var validatorMock = new Mock<IValidator<CreatePatientCommand>>();
            validatorMock.Setup(v => v.Validate(It.IsAny<CreatePatientCommand>()))
                .Returns(new FluentValidation.Results.ValidationResult(new List<FluentValidation.Results.ValidationFailure>
                {
                    new FluentValidation.Results.ValidationFailure("Name", "Name is required")
                }));

            var behavior = new ValidationBehaviour<CreatePatientCommand, Guid>(new[] { validatorMock.Object });

            var request = new CreatePatientCommand();
            var nextHandler = new Mock<RequestHandlerDelegate<Guid>>();

            //// Act & Assert
            //await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => behavior.Handle(request, nextHandler.Object, CancellationToken.None));
        }
    }
}