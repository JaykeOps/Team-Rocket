using Domain.CustomExceptions;
using Domain.Entities;
using Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Repositories
{
    public sealed class GameRepository
    {
        private List<Game> games;
        public static readonly GameRepository instance = new GameRepository();

        private GameRepository()
        {
            //Series series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), "Allsvenskan");

            var matches = DomainService.GetAllMatches();

            var game1 = new Game(matches.ElementAt(0));
            var game2 = new Game(matches.ElementAt(1));
            var game3 = new Game(matches.ElementAt(2));
            var game4 = new Game(matches.ElementAt(3));
            var game5 = new Game(matches.ElementAt(4));
            var game6 = new Game(matches.ElementAt(5));
            var game7 = new Game(matches.ElementAt(6));
            var game8 = new Game(matches.ElementAt(7));
            var game9 = new Game(matches.ElementAt(8));
            var game10 = new Game(matches.ElementAt(9));
            var game11 = new Game(matches.ElementAt(10));
            var game12 = new Game(matches.ElementAt(11));
            var game13 = new Game(matches.ElementAt(12));
            var game14 = new Game(matches.ElementAt(13));
            var game15 = new Game(matches.ElementAt(14));
            var game16 = new Game(matches.ElementAt(15));
            var game17 = new Game(matches.ElementAt(16));
            var game18 = new Game(matches.ElementAt(17));
            var game19 = new Game(matches.ElementAt(18));
            var game20 = new Game(matches.ElementAt(19));
            var game21 = new Game(matches.ElementAt(20));
            var game22 = new Game(matches.ElementAt(21));
            var game23 = new Game(matches.ElementAt(22));
            var game24 = new Game(matches.ElementAt(23));
            var game25 = new Game(matches.ElementAt(24));
            var game26 = new Game(matches.ElementAt(25));
            var game27 = new Game(matches.ElementAt(26));
            var game28 = new Game(matches.ElementAt(27));
            var game29 = new Game(matches.ElementAt(28));
            var game30 = new Game(matches.ElementAt(29));
            var game31 = new Game(matches.ElementAt(30));
            var game32 = new Game(matches.ElementAt(31));
            var game33 = new Game(matches.ElementAt(32));
            var game34 = new Game(matches.ElementAt(33));
            var game35 = new Game(matches.ElementAt(34));
            var game36 = new Game(matches.ElementAt(35));
            var game37 = new Game(matches.ElementAt(36));
            var game38 = new Game(matches.ElementAt(37));
            var game39 = new Game(matches.ElementAt(38));
            var game40 = new Game(matches.ElementAt(39));
            var game41 = new Game(matches.ElementAt(40));
            var game42 = new Game(matches.ElementAt(41));
            var game43 = new Game(matches.ElementAt(42));
            var game44 = new Game(matches.ElementAt(43));
            var game45 = new Game(matches.ElementAt(44));
            var game46 = new Game(matches.ElementAt(45));
            var game47 = new Game(matches.ElementAt(46));
            var game48 = new Game(matches.ElementAt(47));
            var game49 = new Game(matches.ElementAt(48));
            var game50 = new Game(matches.ElementAt(49));
            var game51 = new Game(matches.ElementAt(50));
            var game52 = new Game(matches.ElementAt(51));
            var game53 = new Game(matches.ElementAt(52));
            var game54 = new Game(matches.ElementAt(53));
            var game55 = new Game(matches.ElementAt(54));
            var game56 = new Game(matches.ElementAt(55));
            var game57 = new Game(matches.ElementAt(56));
            var game58 = new Game(matches.ElementAt(57));
            var game59 = new Game(matches.ElementAt(58));
            var game60 = new Game(matches.ElementAt(59));
            var game61 = new Game(matches.ElementAt(60));
            var game62 = new Game(matches.ElementAt(61));
            var game63 = new Game(matches.ElementAt(62));
            var game64 = new Game(matches.ElementAt(63));
            var game65 = new Game(matches.ElementAt(64));
            var game66 = new Game(matches.ElementAt(65));
            var game67 = new Game(matches.ElementAt(66));
            var game68 = new Game(matches.ElementAt(67));
            var game69 = new Game(matches.ElementAt(68));
            var game70 = new Game(matches.ElementAt(69));
            var game71 = new Game(matches.ElementAt(70));
            var game72 = new Game(matches.ElementAt(71));
            var game73 = new Game(matches.ElementAt(72));
            var game74 = new Game(matches.ElementAt(73));
            var game75 = new Game(matches.ElementAt(74));
            var game76 = new Game(matches.ElementAt(75));
            var game77 = new Game(matches.ElementAt(76));
            var game78 = new Game(matches.ElementAt(77));
            var game79 = new Game(matches.ElementAt(78));
            var game80 = new Game(matches.ElementAt(79));
            var game81 = new Game(matches.ElementAt(80));
            var game82 = new Game(matches.ElementAt(81));
            var game83 = new Game(matches.ElementAt(82));
            var game84 = new Game(matches.ElementAt(83));
            var game85 = new Game(matches.ElementAt(84));
            var game86 = new Game(matches.ElementAt(85));
            var game87 = new Game(matches.ElementAt(86));
            var game88 = new Game(matches.ElementAt(87));
            var game89 = new Game(matches.ElementAt(88));
            var game90 = new Game(matches.ElementAt(89));
            var game91 = new Game(matches.ElementAt(90));
            var game92 = new Game(matches.ElementAt(91));
            var game93 = new Game(matches.ElementAt(92));
            var game94 = new Game(matches.ElementAt(93));
            var game95 = new Game(matches.ElementAt(94));
            var game96 = new Game(matches.ElementAt(95));
            var game97 = new Game(matches.ElementAt(96));
            var game98 = new Game(matches.ElementAt(97));
            var game99 = new Game(matches.ElementAt(98));
            var game100 = new Game(matches.ElementAt(99));
            var game101 = new Game(matches.ElementAt(100));
            var game102 = new Game(matches.ElementAt(101));
            var game103 = new Game(matches.ElementAt(102));
            var game104 = new Game(matches.ElementAt(103));
            var game105 = new Game(matches.ElementAt(104));
            var game106 = new Game(matches.ElementAt(105));
            var game107 = new Game(matches.ElementAt(106));
            var game108 = new Game(matches.ElementAt(107));
            var game109 = new Game(matches.ElementAt(108));
            var game110 = new Game(matches.ElementAt(109));
            var game111 = new Game(matches.ElementAt(110));
            var game112 = new Game(matches.ElementAt(111));
            var game113 = new Game(matches.ElementAt(112));
            var game114 = new Game(matches.ElementAt(113));
            var game115 = new Game(matches.ElementAt(114));
            var game116 = new Game(matches.ElementAt(115));
            var game117 = new Game(matches.ElementAt(116));
            var game118 = new Game(matches.ElementAt(117));
            var game119 = new Game(matches.ElementAt(118));
            var game120 = new Game(matches.ElementAt(119));
            var game121 = new Game(matches.ElementAt(120));
            var game122 = new Game(matches.ElementAt(121));
            var game123 = new Game(matches.ElementAt(122));
            var game124 = new Game(matches.ElementAt(123));
            var game125 = new Game(matches.ElementAt(124));
            var game126 = new Game(matches.ElementAt(125));
            var game127 = new Game(matches.ElementAt(126));
            var game128 = new Game(matches.ElementAt(127));
            var game129 = new Game(matches.ElementAt(128));
            var game130 = new Game(matches.ElementAt(129));
            var game131 = new Game(matches.ElementAt(130));
            var game132 = new Game(matches.ElementAt(131));
            var game133 = new Game(matches.ElementAt(132));
            var game134 = new Game(matches.ElementAt(133));
            var game135 = new Game(matches.ElementAt(134));
            var game136 = new Game(matches.ElementAt(135));
            var game137 = new Game(matches.ElementAt(136));
            var game138 = new Game(matches.ElementAt(137));
            var game139 = new Game(matches.ElementAt(138));
            var game140 = new Game(matches.ElementAt(139));
            var game141 = new Game(matches.ElementAt(140));
            var game142 = new Game(matches.ElementAt(141));
            var game143 = new Game(matches.ElementAt(142));
            var game144 = new Game(matches.ElementAt(143));
        }

        public void Add(Game game)
        {
            if (IsAdded(game))
            {
                throw new GameAlreadyAddedException();
            }
            else
            {
                this.games.Add(game);
            }
        }

        public bool IsAdded(Game newGame)
        {
            var isAdded = false;

            foreach (var game in games)
            {
                if (newGame.Id == game.Id)
                {
                    isAdded = true;
                }
            }

            return isAdded;
        }

        public IEnumerable<Game> GetAll()
        {
            return this.games;
        }
    }
}