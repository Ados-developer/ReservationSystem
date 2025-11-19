using ReservationSystem.Mappers;
using ReservationSystem.Models;
using ReservationSystem.Repositories;
using ReservationSystem.Util;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationItemRepository _itemRepository;
        public ReservationService(IReservationRepository reservationRepository, IReservationItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _reservationRepository = reservationRepository;
        }
        public async Task<List<ReservationViewModel>> GetAllAsync()
        {
            List<Reservation> reservations = await _reservationRepository.GetAllAsync();
            return reservations.Select(r => ReservationMapper.ToViewModel(r)).ToList();
        }
        public async Task<ReservationViewModel?> GetByIdAsync(Guid id)
        {
            Reservation? reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                return null;
            }
            ReservationItem? item = await _itemRepository.GetByIdAsync(reservation.ReservationItemId);
            if (item == null)
            {
                return null;
            }
            ReservationViewModel viewModel = ReservationMapper.ToViewModel(reservation);
            viewModel.ReservationItemName = item.ItemName;
            viewModel.ReservationItemDuration = PriceUtil.NumberToEditorString(item.DurationMinutes);
            viewModel.ReservationItemPrice = PriceUtil.GetPriceString(item.ItemPrice);
            return viewModel;
        }
        public async Task CreateAsync(ReservationViewModel reservationViewModel)
        {
            Reservation? reservation = ReservationMapper.ToModel(reservationViewModel);
            reservation.CreatedAt = DateTime.Now;
            await _reservationRepository.AddAsync(reservation);
            await _reservationRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(ReservationViewModel reservationViewModel)
        {
            Reservation? reservation = ReservationMapper.ToModel(reservationViewModel);
            _reservationRepository.Update(reservation);
            await _reservationRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _reservationRepository.DeleteAsync(id);
            await _reservationRepository.SaveChangesAsync();
        }
    }
}
