﻿using AutoMapper;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Application.ViewModels;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class DeliveryAddressService : IDeliveryAddressService
    {
        private readonly IDeliveryAddressRepository deliveryAddressRepository;
        private readonly ISaleOrderRepository saleOrderRepository;
        private readonly ISaleOrderService saleOrderService;
        private readonly IMapper mapper;

        public DeliveryAddressService(IDeliveryAddressRepository deliveryAddressRepository, IMapper mapper,
            ISaleOrderRepository saleOrderRepository, ISaleOrderService saleOrderService)
        {
            this.deliveryAddressRepository = deliveryAddressRepository;
            this.saleOrderRepository = saleOrderRepository;
            this.saleOrderService = saleOrderService;
            this.mapper = mapper;
        }

        public async Task<DeliveryAddressView?> AddDeliveryAddress(Guid userId, AddDeliveryAddressDto addDeliveryAddress)
        {
            var checkEmptyAddress = await deliveryAddressRepository.GetAddressIdByUserId(userId);

            if (checkEmptyAddress == Guid.Empty)
            {
                var addressToCreate = mapper.Map<DeliveryAddress>(addDeliveryAddress);
                addressToCreate.UserId = userId;

                addressToCreate = await deliveryAddressRepository.CreateDeliveryAddressAsync(addressToCreate);

                return mapper.Map<DeliveryAddressView>(addressToCreate);
            }

            var addressToUpdate = mapper.Map<DeliveryAddress>(addDeliveryAddress);

            var updatedAddress = await deliveryAddressRepository.UpdateDeliveryAddressAsync(checkEmptyAddress, addressToUpdate);

            return mapper.Map<DeliveryAddressView>(updatedAddress);
        }


        public async Task<DeliveryAddressView?> DeleteDeliveryAddress(Guid userId)
        {
            var checkExistingAddress = await deliveryAddressRepository.GetAddressIdByUserId(userId);

            if (checkExistingAddress != Guid.Empty)
            {
                var ordersToCancel = await saleOrderRepository.GetPendingOrderIdsByUserIdAsync(userId);

                foreach (var orderId in ordersToCancel)
                {
                    await saleOrderService.CancelOrder(orderId, userId);
                    continue;
                }

                var deletedAddress = await deliveryAddressRepository.DeleteDeliveryAddressAsync(checkExistingAddress);

                return mapper.Map<DeliveryAddressView>(deletedAddress);
            }

            return null;
        }

        public async Task<DeliveryAddressView?> GetDeliveryAddressByUserId(Guid userId)
        {
            var getAddress = await deliveryAddressRepository.GetAddressByUserId(userId);

            if (getAddress != null)
            {
                return mapper.Map<DeliveryAddressView>(getAddress);
            }

            return null;
        }
    }
}