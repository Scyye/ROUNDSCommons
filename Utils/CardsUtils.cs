using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROUNDSCommons.Utils
{
    public class CardsUtils
    {
        public static IEnumerable<CardInfo> GetCardsWithName(string name)
        {
            return ModdingUtils.Utils.Cards.all.Where(card => card.name == name);
        }

        public static IEnumerable<CardInfo> GetActiveCardsWithName(string name)
        {
            return ModdingUtils.Utils.Cards.active.Where(card => card.name == name);
        }

        public static Card GetCardFromClosestName(string name)
        {
            name = name.ToLower();
            CommonsPlugin.instance.logger.Log("Card name: " + name);
            List<Card> matchingCards = new List<Card>();
            foreach (Card c in Enum.GetValues(typeof(Card)))
            {
                CardInfo cardInfo = GetCardInfoOfCard(c);

                if (cardInfo.cardName == name)
                {
                    CommonsPlugin.instance.logger.Log("Given card name: " + name);
                    return c;
                }
                if (cardInfo.cardName.StartsWith(name))
                {
                    matchingCards.Add(c);
                }
            }

            if (matchingCards.Count > 0)
            {
                CommonsPlugin.instance.logger.Log("Given card name: NULL");
                return Card.NULL;
            }

            CommonsPlugin.instance.logger.Log("Given card name: " + matchingCards.First().ToString());
            return matchingCards.First();
        }

        public static Card[] cards = (Card[])Enum.GetValues(typeof(Card));
        public static CardInfo GetCardInfoOfCard(Card card)
        {
            return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(card.ToString());
        }

        public static CardInfo GetCardInfoOfCard(string cardName)
        {
            return GetCardInfoOfCard(GetCardFromClosestName(cardName));
        }
    }

    public enum Card
    {
        AbyssalCountdown,
        Barrage,
        Big_Bullet,
        BombsAway,
        Bouncy,
        Brawler,
        Buckshot,
        Burst,
        Careful_planning,
        Chase,
        ChillingPresence,
        Cold_bullets,
        Combine,
        Dazzle,
        Decay,
        Defender,
        Demonic_pact,
        DrillAmmo,
        Echo,
        EMP,
        Empower,
        Explosive_bullet,
        Fast_ball,
        Fast_forward,
        Frost_slam,
        Glasscannon,
        Grow,
        Healing_field,
        Homing,
        HUGE,
        Implode,
        Leach, // Dont even ask, idfk.
        Lifestealer,
        Mayhem,
        Overpower,
        Parasite,
        Phoenix,
        Poison_bullets,
        Pristine_perseverence,
        Quick_Reload,
        Quick_Shot,
        RadarShot,
        Radiance,
        Refresh,
        Remote,
        Riccochet,
        Saw,
        Scavenger,
        Shield_Charge,
        Sheilds_up,
        Shockwave,
        Silence,
        Sneaky_bullets,
        SPRAY,
        Static_field,
        Steady_shot,
        Supernova,
        Tactical_reload,
        Tank,
        TargetBounce,
        TasteOfBlood,
        Teleport,
        Thruster,
        Timed_detonation,
        Toxic_cloud,
        Trickster,
        Wind_up,



        NULL // THIS ISN'T A NULLED CARD, THIS IS IF NO CARD WAS FOUND.
    }

}
