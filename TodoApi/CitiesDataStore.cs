using System;
using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDto> Cities
        {
            get;
            set;
        }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "Ciudad de EEUU",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() {
                            Id = 1,
                            Name = "Central Park",
                            Description = "Ciudad muy visitada"
                        },
                        new PointOfInterestDto() {
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "A 102-Story"
                        }

                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Buenos Aires",
                    Description = "Ciudad de Argentina",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() {
                            Id = 3,
                            Name = "Obelisco",
                            Description = "Ubicado en el corazón de la ciudad"
                        },
                        new PointOfInterestDto() {
                            Id = 4,
                            Name = "Casa rosada",
                            Description = "Casa Emblematica de Buenos Aires"
                        }

                    }

                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Madrid",
                    Description = "Ciudad de España",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() {
                            Id = 5,
                            Name = "Palacio Real de Madrid",
                            Description = "El Palacio Real de Madrid es la residencia oficial del rey de España; no obstante, los actuales reyes no habitan en él, sino en el Palacio de la Zarzuela, por lo que es utilizado para ceremonias de Estado y actos solemnes"
                        }

                    }
                }
            };
        }
    }
}

