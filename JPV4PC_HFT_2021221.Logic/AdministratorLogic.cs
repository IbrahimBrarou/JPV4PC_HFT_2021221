using JPV4PC_HFT_2021221.Models;
using JPV4PC_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public class AdministratorLogic : IAdministratorLogic
    {
        private readonly IServicesRepository _ServicesRepository;
        private readonly IReservationsRepository _ReservationsRepository;
        private readonly IFansRepository _FansRepository;
        private readonly IArtistsRepository _ArtistRepository;
        private readonly IReservationsServicesRepository _ReservationsServicesConnectionRepository;
        public AdministratorLogic(IServicesRepository servicesRepository, IReservationsRepository reservationsRepository, IFansRepository fansRepository, IArtistsRepository artistRepository, IReservationsServicesRepository reservationsServicesConnectionRepository)
        {
            _ServicesRepository = servicesRepository;
            _ReservationsRepository = reservationsRepository;
            _FansRepository = fansRepository;
            _ArtistRepository = artistRepository;
            _ReservationsServicesConnectionRepository = reservationsServicesConnectionRepository;
        }

        
        
        
    }
}
