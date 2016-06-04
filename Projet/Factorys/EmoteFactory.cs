using BusinessLayer.Entities;
using Projet.Modeles;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Projet.Extension;
using System.Linq;
using System.IO;
using System;
using System.Windows.Media.Imaging;

namespace Projet.Factorys {
    public static class EmoteFactory {
        public static Emote EmoteEntitiesToEmoteModele(EmoteEntities emote) {
            return new Emote {
                Nom = emote.Nom,
                Description = emote.Description,
                Image = new BitmapImage(new Uri(Path.GetFullPath(emote.Image))),
            };
        }

        public static ObservableCollection<Emote> AllEmoteEntitieToEmote(List<EmoteEntities> list) {
            return list.Select(EmoteEntitiesToEmoteModele).ToObservableCollection();
        }

    }
}
