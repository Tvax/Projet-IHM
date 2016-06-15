using BusinessLayer.Entities;
using System.Collections.Generic;

namespace BusinessLayer {
    public static class EmoteDAO {
        public static List<EmoteEntities> GetAllEmote() {
            return new List<EmoteEntities>()
            {
                new EmoteEntities {Nom = "Default_Name", Description = "Default_Description", Origine = "Default_Origin", Abonnement= "Default_Sub", EmoteMin= 1852, Image = "no_image.png"},
            };

        }
    }
}
