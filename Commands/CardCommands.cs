using RarityLib;
using ROUNDSCommons.Utils;
using System.Linq;

namespace ROUNDSCommons.Commands
{
    public class CardCommands
    {
        public class GiveCard : Command
        {
            public override CommandDetails Details => new CommandDetails()
            {
                Name = "card-give"
            };

            public override CommandResponse Execute(CommandEvent e)
            {
                if (e.args.Length < 2)
                {
                    // Handle insufficient arguments error
                    return CommandResponse.DefaultNoSuccess;
                }

                var playerName = e.args[0];
                var cardName = e.args[1];

                if (string.IsNullOrEmpty(cardName))
                {
                    // Handle empty cardName error
                    return CommandResponse.DefaultNoSuccess;
                }

                Player target = PlayerUtils.GetPlayerFromName(playerName);
                CardInfo card = CardsUtils.GetCardInfoOfCard(CardsUtils.GetCardFromClosestName(cardName));

                if (card == null)
                    card = CardsUtils.GetCardInfoOfCard(Card.Tank);

                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(target, card, false,
                    "" + cardName.ToCharArray()[0] + cardName.ToCharArray()[1], 0, 0);

                return target.data.currentCards.Contains(card) ? CommandResponse.DefaultSuccess:CommandResponse.DefaultNoSuccess;
            }
        }
    }
}