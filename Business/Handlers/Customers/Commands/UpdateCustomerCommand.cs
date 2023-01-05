﻿using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Customers.Commands
{
    public class UpdateCustomerCommand:IRequest<IResult>
    {
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public string MobilePhones { get; set; }
        public string Email { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedConsumerId { get; set; }

        public class UpdateCustomerCommandHandler:IRequestHandler<UpdateCustomerCommand,IResult>
        {
            private readonly ICustomerRepository _customerRepository;


            public  UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
            {
                _customerRepository = customerRepository;
            }
             public async Task<IResult>Handle(UpdateCustomerCommand request,CancellationToken cancellationToken)
            {
                var isThereAnyCustomer=await _customerRepository.GetAsync(c=>c.CustomerId==request.CustomerId);

                isThereAnyCustomer.CustomerName=request.CustomerName;
                isThereAnyCustomer.Address=request.Address;
                isThereAnyCustomer.MobilePhones=request.MobilePhones;
                isThereAnyCustomer.Email=request.Email;
                isThereAnyCustomer.LastUpdatedConsumerId=request.LastUpdatedConsumerId;
                isThereAnyCustomer.LastUpdatedDate=request.LastUpdatedDate;

                _customerRepository.Update(isThereAnyCustomer);
                await _customerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);

            }





        }
    }
}