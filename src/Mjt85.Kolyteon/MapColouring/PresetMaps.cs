namespace Mjt85.Kolyteon.MapColouring;

/// <summary>
///     Contains preset maps based on real-world geography.
/// </summary>
public static class PresetMaps
{
    /// <summary>
    ///     Creates and returns a new <see cref="PresetMap" /> based on the larger states and territories of Australia.
    /// </summary>
    /// <remarks>
    ///     This preset map has 7 regions split between 2 disconnected sub-maps, with 9 pairs of neighbouring regions. The
    ///     preset map and its members are created every time this method is invoked.
    /// </remarks>
    /// <returns>A new <see cref="PresetMap" /> instance.</returns>
    public static PresetMap Australia()
    {
        Region NSW = Region.FromId("NSW");
        Region NT = Region.FromId("NT");
        Region Q = Region.FromId("Q");
        Region SA = Region.FromId("SA");
        Region T = Region.FromId("T");
        Region V = Region.FromId("V");
        Region WA = Region.FromId("WA");

        return new PresetMap
        {
            Regions =
            [
                NSW, NT, Q, SA, T,
                V, WA
            ],
            NeighbourPairs =
            [
                new NeighbourPair(NSW, Q),
                new NeighbourPair(NSW, SA),
                new NeighbourPair(NSW, V),
                new NeighbourPair(NT, Q),
                new NeighbourPair(NT, SA),
                new NeighbourPair(NT, WA),
                new NeighbourPair(Q, SA),
                new NeighbourPair(SA, V),
                new NeighbourPair(SA, WA)
            ]
        };
    }

    /// <summary>
    ///     Creates and returns a new <see cref="PresetMap" /> based on the provinces and territories of Canada.
    /// </summary>
    /// <remarks>
    ///     This preset map has 14 regions split between 2 disconnected sub-maps, with 15 pairs of neighbouring regions.
    ///     The preset map and its members are created every time this method is invoked.
    /// </remarks>
    /// <returns>A new <see cref="PresetMap" /> instance.</returns>
    public static PresetMap Canada()
    {
        Region AB = Region.FromId("AB");
        Region BC = Region.FromId("BC");
        Region MB = Region.FromId("MB");
        Region NB = Region.FromId("NB");
        Region NL = Region.FromId("NL");
        Region NS = Region.FromId("NS");
        Region NT = Region.FromId("NT");
        Region NU = Region.FromId("NU");
        Region ON = Region.FromId("ON");
        Region PE = Region.FromId("PE");
        Region QC = Region.FromId("QC");
        Region SK = Region.FromId("SK");
        Region YT = Region.FromId("YT");

        return new PresetMap
        {
            Regions =
            [
                AB, BC, MB, NB, NL,
                NS, NT, NU, ON, PE,
                QC, SK, YT
            ],
            NeighbourPairs =
            [
                new NeighbourPair(AB, BC),
                new NeighbourPair(AB, NT),
                new NeighbourPair(AB, SK),
                new NeighbourPair(BC, NT),
                new NeighbourPair(BC, YT),
                new NeighbourPair(MB, NU),
                new NeighbourPair(MB, ON),
                new NeighbourPair(MB, SK),
                new NeighbourPair(NB, NS),
                new NeighbourPair(NB, QC),
                new NeighbourPair(NL, QC),
                new NeighbourPair(NT, NU),
                new NeighbourPair(NT, SK),
                new NeighbourPair(NT, YT),
                new NeighbourPair(ON, QC)
            ]
        };
    }

    /// <summary>
    ///     Creates and returns a new <see cref="PresetMap" /> based on the wards of the City of London.
    /// </summary>
    /// <remarks>
    ///     This preset map has 25 regions forming a single connected map, with 56 pairs of neighbouring regions. The
    ///     preset map and its members are created every time this method is invoked.
    /// </remarks>
    /// <returns>A new <see cref="PresetMap" /> instance.</returns>
    public static PresetMap CityOfLondon()
    {
        Region Aldersgate = Region.FromId("Aldersgate");
        Region Aldgate = Region.FromId("Aldgate");
        Region Bassishaw = Region.FromId("Bassishaw");
        Region Billingsgate = Region.FromId("Billingsgate");
        Region Bishopsgate = Region.FromId("Bishopsgate");
        Region BreadStreet = Region.FromId("BreadStreet");
        Region Bridge = Region.FromId("Bridge");
        Region BroadStreet = Region.FromId("BroadStreet");
        Region Candlewick = Region.FromId("Candlewick");
        Region CastleBaynard = Region.FromId("CastleBaynard");
        Region Cheap = Region.FromId("Cheap");
        Region ColemanStreet = Region.FromId("ColemanStreet");
        Region Cordwainer = Region.FromId("Cordwainer");
        Region Cornhill = Region.FromId("Cornhill");
        Region Cripplegate = Region.FromId("Cripplegate");
        Region Dowgate = Region.FromId("Dowgate");
        Region FarringdonWithin = Region.FromId("FarringdonWithin");
        Region FarringdonWithout = Region.FromId("FarringdonWithout");
        Region Langbourn = Region.FromId("Langbourn");
        Region LimeStreet = Region.FromId("LimeStreet");
        Region Portsoken = Region.FromId("Portsoken");
        Region Queenhithe = Region.FromId("Queenhithe");
        Region Tower = Region.FromId("Tower");
        Region Vintry = Region.FromId("Vintry");
        Region Walbrook = Region.FromId("Walbrook");

        return new PresetMap
        {
            Regions =
            [
                Aldersgate, Aldgate, Bassishaw, Billingsgate, Bishopsgate,
                BreadStreet, Bridge, BroadStreet, Candlewick, CastleBaynard,
                Cheap, ColemanStreet, Cordwainer, Cornhill, Cripplegate,
                Dowgate, FarringdonWithin, FarringdonWithout, Langbourn, LimeStreet,
                Portsoken, Queenhithe, Tower, Vintry, Walbrook
            ],
            NeighbourPairs =
            [
                new NeighbourPair(Aldersgate, Cripplegate),
                new NeighbourPair(Aldersgate, FarringdonWithin),
                new NeighbourPair(Aldgate, Bishopsgate),
                new NeighbourPair(Aldgate, Langbourn),
                new NeighbourPair(Aldgate, LimeStreet),
                new NeighbourPair(Aldgate, Portsoken),
                new NeighbourPair(Aldgate, Tower),
                new NeighbourPair(Bassishaw, Cheap),
                new NeighbourPair(Bassishaw, ColemanStreet),
                new NeighbourPair(Bassishaw, Cripplegate),
                new NeighbourPair(Billingsgate, Bridge),
                new NeighbourPair(Billingsgate, Langbourn),
                new NeighbourPair(Billingsgate, Tower),
                new NeighbourPair(Bishopsgate, BroadStreet),
                new NeighbourPair(Bishopsgate, ColemanStreet),
                new NeighbourPair(Bishopsgate, Cornhill),
                new NeighbourPair(Bishopsgate, LimeStreet),
                new NeighbourPair(Bishopsgate, Portsoken),
                new NeighbourPair(BreadStreet, CastleBaynard),
                new NeighbourPair(BreadStreet, Cheap),
                new NeighbourPair(BreadStreet, Cordwainer),
                new NeighbourPair(BreadStreet, FarringdonWithin),
                new NeighbourPair(BreadStreet, Queenhithe),
                new NeighbourPair(BreadStreet, Vintry),
                new NeighbourPair(Bridge, Candlewick),
                new NeighbourPair(Bridge, Dowgate),
                new NeighbourPair(Bridge, Langbourn),
                new NeighbourPair(BroadStreet, ColemanStreet),
                new NeighbourPair(BroadStreet, Cornhill),
                new NeighbourPair(BroadStreet, Walbrook),
                new NeighbourPair(Candlewick, Dowgate),
                new NeighbourPair(Candlewick, Langbourn),
                new NeighbourPair(Candlewick, Walbrook),
                new NeighbourPair(CastleBaynard, FarringdonWithin),
                new NeighbourPair(CastleBaynard, FarringdonWithout),
                new NeighbourPair(CastleBaynard, Queenhithe),
                new NeighbourPair(Cheap, ColemanStreet),
                new NeighbourPair(Cheap, Cordwainer),
                new NeighbourPair(Cheap, FarringdonWithin),
                new NeighbourPair(Cheap, Walbrook),
                new NeighbourPair(ColemanStreet, Cripplegate),
                new NeighbourPair(ColemanStreet, Walbrook),
                new NeighbourPair(Cordwainer, Dowgate),
                new NeighbourPair(Cordwainer, Vintry),
                new NeighbourPair(Cordwainer, Walbrook),
                new NeighbourPair(Cornhill, Langbourn),
                new NeighbourPair(Cornhill, LimeStreet),
                new NeighbourPair(Cornhill, Walbrook),
                new NeighbourPair(Dowgate, Vintry),
                new NeighbourPair(Dowgate, Walbrook),
                new NeighbourPair(FarringdonWithin, FarringdonWithout),
                new NeighbourPair(Langbourn, LimeStreet),
                new NeighbourPair(Langbourn, Tower),
                new NeighbourPair(Langbourn, Walbrook),
                new NeighbourPair(Portsoken, Tower),
                new NeighbourPair(Queenhithe, Vintry)
            ]
        };
    }

    /// <summary>
    ///     Creates and returns a new <see cref="PresetMap" /> based on the prefectures of Japan.
    /// </summary>
    /// <remarks>
    ///     This preset map has 47 regions split between 5 disconnected sub-maps, with 84 pairs of neighbouring regions.
    ///     The preset map and its members are created every time this method is invoked.
    /// </remarks>
    /// <returns>A new <see cref="PresetMap" /> instance.</returns>
    public static PresetMap Japan()
    {
        Region Aichi = Region.FromId("Aichi");
        Region Akita = Region.FromId("Akita");
        Region Aomori = Region.FromId("Aomori");
        Region Chiba = Region.FromId("Chiba");
        Region Ehime = Region.FromId("Ehime");
        Region Fukui = Region.FromId("Fukui");
        Region Fukuoka = Region.FromId("Fukuoka");
        Region Fukushima = Region.FromId("Fukushima");
        Region Gifu = Region.FromId("Gifu");
        Region Gunma = Region.FromId("Gunma");
        Region Hiroshima = Region.FromId("Hiroshima");
        Region Hokkaido = Region.FromId("Hokkaido");
        Region Hyogo = Region.FromId("Hyogo");
        Region Ibaraki = Region.FromId("Ibaraki");
        Region Ishikawa = Region.FromId("Ishikawa");
        Region Iwate = Region.FromId("Iwate");
        Region Kagawa = Region.FromId("Kagawa");
        Region Kagoshima = Region.FromId("Kagoshima");
        Region Kanagawa = Region.FromId("Kanagawa");
        Region Kochi = Region.FromId("Kochi");
        Region Kumamoto = Region.FromId("Kumamoto");
        Region Kyoto = Region.FromId("Kyoto");
        Region Mie = Region.FromId("Mie");
        Region Miyagi = Region.FromId("Miyagi");
        Region Miyazaki = Region.FromId("Miyazaki");
        Region Nagano = Region.FromId("Nagano");
        Region Nagasaki = Region.FromId("Nagasaki");
        Region Nara = Region.FromId("Nara");
        Region Niigata = Region.FromId("Niigata");
        Region Oita = Region.FromId("Oita");
        Region Okayama = Region.FromId("Okayama");
        Region Okinawa = Region.FromId("Okinawa");
        Region Osaka = Region.FromId("Osaka");
        Region Saga = Region.FromId("Saga");
        Region Saitama = Region.FromId("Saitama");
        Region Shiga = Region.FromId("Shiga");
        Region Shimane = Region.FromId("Shimane");
        Region Shizuoka = Region.FromId("Shizuoka");
        Region Tochigi = Region.FromId("Tochigi");
        Region Tokushima = Region.FromId("Tokushima");
        Region Tokyo = Region.FromId("Tokyo");
        Region Tottori = Region.FromId("Tottori");
        Region Toyama = Region.FromId("Toyama");
        Region Wakayama = Region.FromId("Wakayama");
        Region Yamagata = Region.FromId("Yamagata");
        Region Yamaguchi = Region.FromId("Yamaguchi");
        Region Yamanashi = Region.FromId("Yamanashi");

        return new PresetMap
        {
            Regions =
            [
                Aichi, Akita, Aomori, Chiba, Ehime,
                Fukui, Fukuoka, Fukushima, Gifu, Gunma,
                Hiroshima, Hokkaido, Hyogo, Ibaraki, Ishikawa,
                Iwate, Kagawa, Kagoshima, Kanagawa, Kochi,
                Kumamoto, Kyoto, Mie, Miyagi, Miyazaki,
                Nagano, Nagasaki, Nara, Niigata, Oita,
                Okayama, Okinawa, Osaka, Saga, Saitama,
                Shiga, Shimane, Shizuoka, Tochigi, Tokushima,
                Tokyo, Tottori, Toyama, Wakayama, Yamagata,
                Yamaguchi, Yamanashi
            ],
            NeighbourPairs =
            [
                new NeighbourPair(Aichi, Gifu),
                new NeighbourPair(Aichi, Mie),
                new NeighbourPair(Aichi, Nagano),
                new NeighbourPair(Aichi, Shizuoka),
                new NeighbourPair(Akita, Aomori),
                new NeighbourPair(Akita, Iwate),
                new NeighbourPair(Akita, Miyagi),
                new NeighbourPair(Akita, Yamagata),
                new NeighbourPair(Aomori, Iwate),
                new NeighbourPair(Chiba, Ibaraki),
                new NeighbourPair(Chiba, Saitama),
                new NeighbourPair(Chiba, Tokyo),
                new NeighbourPair(Ehime, Kagawa),
                new NeighbourPair(Ehime, Kochi),
                new NeighbourPair(Ehime, Tokushima),
                new NeighbourPair(Fukui, Gifu),
                new NeighbourPair(Fukui, Ishikawa),
                new NeighbourPair(Fukui, Kyoto),
                new NeighbourPair(Fukui, Shiga),
                new NeighbourPair(Fukuoka, Kumamoto),
                new NeighbourPair(Fukuoka, Oita),
                new NeighbourPair(Fukuoka, Saga),
                new NeighbourPair(Fukushima, Gunma),
                new NeighbourPair(Fukushima, Ibaraki),
                new NeighbourPair(Fukushima, Miyagi),
                new NeighbourPair(Fukushima, Niigata),
                new NeighbourPair(Fukushima, Tochigi),
                new NeighbourPair(Fukushima, Yamagata),
                new NeighbourPair(Gifu, Ishikawa),
                new NeighbourPair(Gifu, Mie),
                new NeighbourPair(Gifu, Nagano),
                new NeighbourPair(Gifu, Shiga),
                new NeighbourPair(Gifu, Toyama),
                new NeighbourPair(Gunma, Nagano),
                new NeighbourPair(Gunma, Niigata),
                new NeighbourPair(Gunma, Saitama),
                new NeighbourPair(Gunma, Tochigi),
                new NeighbourPair(Hiroshima, Okayama),
                new NeighbourPair(Hiroshima, Shimane),
                new NeighbourPair(Hiroshima, Tottori),
                new NeighbourPair(Hiroshima, Yamaguchi),
                new NeighbourPair(Hyogo, Kyoto),
                new NeighbourPair(Hyogo, Okayama),
                new NeighbourPair(Hyogo, Osaka),
                new NeighbourPair(Hyogo, Tottori),
                new NeighbourPair(Ibaraki, Saitama),
                new NeighbourPair(Ibaraki, Tochigi),
                new NeighbourPair(Ishikawa, Toyama),
                new NeighbourPair(Iwate, Miyagi),
                new NeighbourPair(Kagawa, Tokushima),
                new NeighbourPair(Kagoshima, Kumamoto),
                new NeighbourPair(Kagoshima, Miyazaki),
                new NeighbourPair(Kanagawa, Shizuoka),
                new NeighbourPair(Kanagawa, Tokyo),
                new NeighbourPair(Kanagawa, Yamanashi),
                new NeighbourPair(Kochi, Tokushima),
                new NeighbourPair(Kumamoto, Miyazaki),
                new NeighbourPair(Kumamoto, Oita),
                new NeighbourPair(Kyoto, Nara),
                new NeighbourPair(Kyoto, Osaka),
                new NeighbourPair(Kyoto, Shiga),
                new NeighbourPair(Mie, Nara),
                new NeighbourPair(Mie, Shiga),
                new NeighbourPair(Mie, Wakayama),
                new NeighbourPair(Miyagi, Yamagata),
                new NeighbourPair(Miyazaki, Oita),
                new NeighbourPair(Nagano, Niigata),
                new NeighbourPair(Nagano, Shizuoka),
                new NeighbourPair(Nagano, Toyama),
                new NeighbourPair(Nagano, Yamanashi),
                new NeighbourPair(Nagasaki, Saga),
                new NeighbourPair(Nara, Osaka),
                new NeighbourPair(Nara, Wakayama),
                new NeighbourPair(Niigata, Toyama),
                new NeighbourPair(Niigata, Yamagata),
                new NeighbourPair(Okayama, Tottori),
                new NeighbourPair(Osaka, Wakayama),
                new NeighbourPair(Saitama, Tochigi),
                new NeighbourPair(Saitama, Tokyo),
                new NeighbourPair(Saitama, Yamanashi),
                new NeighbourPair(Shimane, Tottori),
                new NeighbourPair(Shimane, Yamaguchi),
                new NeighbourPair(Shizuoka, Yamanashi),
                new NeighbourPair(Tokyo, Yamanashi)
            ]
        };
    }

    /// <summary>
    ///     Creates and returns a new <see cref="PresetMap" /> based on the arondissements of the city of Paris.
    /// </summary>
    /// <remarks>
    ///     This preset map has 20 regions forming a single connected map, with 44 pairs of neighbouring regions. The
    ///     preset map and its members are created every time this method is invoked.
    /// </remarks>
    /// <returns>A new <see cref="PresetMap" /> instance.</returns>
    public static PresetMap Paris()
    {
        Region A01 = Region.FromId("A01");
        Region A02 = Region.FromId("A02");
        Region A03 = Region.FromId("A03");
        Region A04 = Region.FromId("A04");
        Region A05 = Region.FromId("A05");
        Region A06 = Region.FromId("A06");
        Region A07 = Region.FromId("A07");
        Region A08 = Region.FromId("A08");
        Region A09 = Region.FromId("A09");
        Region A10 = Region.FromId("A10");
        Region A11 = Region.FromId("A11");
        Region A12 = Region.FromId("A12");
        Region A13 = Region.FromId("A13");
        Region A14 = Region.FromId("A14");
        Region A15 = Region.FromId("A15");
        Region A16 = Region.FromId("A16");
        Region A17 = Region.FromId("A17");
        Region A18 = Region.FromId("A18");
        Region A19 = Region.FromId("A19");
        Region A20 = Region.FromId("A20");

        return new PresetMap
        {
            Regions =
            [
                A01, A02, A03, A04, A05,
                A06, A07, A08, A09, A10,
                A11, A12, A13, A14, A15,
                A16, A17, A18, A19, A20
            ],
            NeighbourPairs =
            [
                new NeighbourPair(A01, A02),
                new NeighbourPair(A01, A03),
                new NeighbourPair(A01, A04),
                new NeighbourPair(A01, A06),
                new NeighbourPair(A01, A07),
                new NeighbourPair(A01, A08),
                new NeighbourPair(A01, A09),
                new NeighbourPair(A02, A03),
                new NeighbourPair(A02, A09),
                new NeighbourPair(A02, A10),
                new NeighbourPair(A03, A04),
                new NeighbourPair(A03, A10),
                new NeighbourPair(A03, A11),
                new NeighbourPair(A04, A05),
                new NeighbourPair(A04, A11),
                new NeighbourPair(A04, A12),
                new NeighbourPair(A05, A06),
                new NeighbourPair(A05, A13),
                new NeighbourPair(A05, A14),
                new NeighbourPair(A06, A07),
                new NeighbourPair(A06, A14),
                new NeighbourPair(A06, A15),
                new NeighbourPair(A07, A08),
                new NeighbourPair(A07, A15),
                new NeighbourPair(A07, A16),
                new NeighbourPair(A08, A09),
                new NeighbourPair(A08, A16),
                new NeighbourPair(A08, A17),
                new NeighbourPair(A09, A10),
                new NeighbourPair(A09, A18),
                new NeighbourPair(A10, A11),
                new NeighbourPair(A10, A18),
                new NeighbourPair(A10, A19),
                new NeighbourPair(A11, A12),
                new NeighbourPair(A11, A20),
                new NeighbourPair(A12, A13),
                new NeighbourPair(A12, A20),
                new NeighbourPair(A13, A14),
                new NeighbourPair(A14, A15),
                new NeighbourPair(A15, A16),
                new NeighbourPair(A16, A17),
                new NeighbourPair(A17, A18),
                new NeighbourPair(A18, A19),
                new NeighbourPair(A19, A20)
            ]
        };
    }

    /// <summary>
    ///     Creates and returns a new <see cref="PresetMap" /> based on the provinces of Rwanda.
    /// </summary>
    /// <remarks>
    ///     This preset map has 5 regions forming a single connected map, with 8 pairs of neighbouring regions. The preset
    ///     map and its members are created every time this method is invoked.
    /// </remarks>
    /// <returns>A new <see cref="PresetMap" /> instance.</returns>
    public static PresetMap Rwanda()
    {
        Region E = Region.FromId("E");
        Region K = Region.FromId("K");
        Region N = Region.FromId("N");
        Region S = Region.FromId("S");
        Region W = Region.FromId("W");

        return new PresetMap
        {
            Regions =
            [
                E, K, N, S, W
            ],
            NeighbourPairs =
            [
                new NeighbourPair(E, K),
                new NeighbourPair(E, N),
                new NeighbourPair(E, S),
                new NeighbourPair(K, N),
                new NeighbourPair(K, S),
                new NeighbourPair(N, S),
                new NeighbourPair(N, W),
                new NeighbourPair(S, W)
            ]
        };
    }

    /// <summary>
    ///     Creates and returns a new <see cref="PresetMap" /> based on the castelli of San Marino.
    /// </summary>
    /// <remarks>
    ///     This preset map has 9 regions forming a single connected map, with 15 pairs of neighbouring regions. The
    ///     preset map and its members are created every time this method is invoked.
    /// </remarks>
    /// <returns>A new <see cref="PresetMap" /> instance.</returns>
    public static PresetMap SanMarino()
    {
        Region AC = Region.FromId("AC");
        Region BM = Region.FromId("BM");
        Region CN = Region.FromId("CN");
        Region DO = Region.FromId("DO");
        Region FA = Region.FromId("FA");
        Region FI = Region.FromId("FI");
        Region MG = Region.FromId("MG");
        Region SE = Region.FromId("SE");
        Region SM = Region.FromId("SM");

        return new PresetMap
        {
            Regions =
            [
                AC, BM, CN, DO, FA,
                FI, MG, SE, SM
            ],
            NeighbourPairs =
            [
                new NeighbourPair(AC, BM),
                new NeighbourPair(AC, SM),
                new NeighbourPair(BM, DO),
                new NeighbourPair(BM, FA),
                new NeighbourPair(BM, FI),
                new NeighbourPair(BM, SE),
                new NeighbourPair(BM, SM),
                new NeighbourPair(CN, FI),
                new NeighbourPair(CN, SM),
                new NeighbourPair(DO, FA),
                new NeighbourPair(DO, SE),
                new NeighbourPair(FA, FI),
                new NeighbourPair(FA, MG),
                new NeighbourPair(FI, MG),
                new NeighbourPair(FI, SM)
            ]
        };
    }

    /// <summary>
    ///     Creates and returns a new <see cref="PresetMap" /> based on the United Kingdom Shipping Forecast.
    /// </summary>
    /// <remarks>
    ///     This preset map has 31 regions forming a single connected map, with 25 pairs of neighbouring regions. The
    ///     preset map and its members are created every time this method is invoked.
    /// </remarks>
    /// <returns>A new <see cref="PresetMap" /> instance.</returns>
    public static PresetMap UKShippingForecast()
    {
        Region Bailey = Region.FromId("Bailey");
        Region Biscay = Region.FromId("Biscay");
        Region Cromarty = Region.FromId("Cromarty");
        Region Dogger = Region.FromId("Dogger");
        Region Dover = Region.FromId("Dover");
        Region Faeroes = Region.FromId("Faeroes");
        Region FairIsle = Region.FromId("FairIsle");
        Region Fastnet = Region.FromId("Fastnet");
        Region Fisher = Region.FromId("Fisher");
        Region FitzRoy = Region.FromId("FitzRoy");
        Region Forth = Region.FromId("Forth");
        Region Forties = Region.FromId("Forties");
        Region GermanBight = Region.FromId("GermanBight");
        Region Hebrides = Region.FromId("Hebrides");
        Region Humber = Region.FromId("Humber");
        Region IrishSea = Region.FromId("IrishSea");
        Region Lundy = Region.FromId("Lundy");
        Region Malin = Region.FromId("Malin");
        Region NorthUtsire = Region.FromId("NorthUtsire");
        Region Plymouth = Region.FromId("Plymouth");
        Region Portland = Region.FromId("Portland");
        Region Rockall = Region.FromId("Rockall");
        Region Shannon = Region.FromId("Shannon");
        Region Sole = Region.FromId("Sole");
        Region SoutheastIceland = Region.FromId("SoutheastIceland");
        Region SouthUtsire = Region.FromId("SouthUtsire");
        Region Thames = Region.FromId("Thames");
        Region Trafalgar = Region.FromId("Trafalgar");
        Region Tyne = Region.FromId("Tyne");
        Region Viking = Region.FromId("Viking");
        Region Wight = Region.FromId("Wight");

        return new PresetMap
        {
            Regions =
            [
                Bailey, Biscay, Cromarty, Dogger, Dover,
                Faeroes, FairIsle, Fastnet, Fisher, FitzRoy,
                Forth, Forties, GermanBight, Hebrides, Humber,
                IrishSea, Lundy, Malin, NorthUtsire, Plymouth,
                Portland, Rockall, Shannon, Sole, SoutheastIceland,
                SouthUtsire, Thames, Trafalgar, Tyne, Viking,
                Wight
            ],
            NeighbourPairs =
            [
                new NeighbourPair(Fisher, GermanBight),
                new NeighbourPair(Fisher, SouthUtsire),
                new NeighbourPair(FitzRoy, Sole),
                new NeighbourPair(FitzRoy, Trafalgar),
                new NeighbourPair(Forth, Forties),
                new NeighbourPair(Forth, Tyne),
                new NeighbourPair(Forties, SouthUtsire),
                new NeighbourPair(Forties, Viking),
                new NeighbourPair(GermanBight, Humber),
                new NeighbourPair(Hebrides, Malin),
                new NeighbourPair(Hebrides, Rockall),
                new NeighbourPair(Humber, Thames),
                new NeighbourPair(Humber, Tyne),
                new NeighbourPair(IrishSea, Lundy),
                new NeighbourPair(IrishSea, Malin),
                new NeighbourPair(Lundy, Plymouth),
                new NeighbourPair(Malin, Rockall),
                new NeighbourPair(NorthUtsire, SouthUtsire),
                new NeighbourPair(NorthUtsire, Viking),
                new NeighbourPair(Plymouth, Portland),
                new NeighbourPair(Plymouth, Sole),
                new NeighbourPair(Portland, Wight),
                new NeighbourPair(Rockall, Shannon),
                new NeighbourPair(Shannon, Sole),
                new NeighbourPair(SouthUtsire, Viking)
            ]
        };
    }

    /// <summary>
    ///     Creates and returns a new <see cref="PresetMap" /> based on the states, federal district, and inhabited territories
    ///     of the United States of America.
    /// </summary>
    /// <remarks>
    ///     This preset map has 57 regions split between 8 disconnected sub-maps, with 107 pairs of neighbouring regions.
    ///     The preset map and its members are created every time this method is invoked.
    /// </remarks>
    /// <returns>A new <see cref="PresetMap" /> instance.</returns>
    public static PresetMap UnitedStates()
    {
        Region AK = Region.FromId("AK");
        Region AL = Region.FromId("AL");
        Region AR = Region.FromId("AR");
        Region AS = Region.FromId("AS");
        Region AZ = Region.FromId("AZ");
        Region CA = Region.FromId("CA");
        Region CO = Region.FromId("CO");
        Region CT = Region.FromId("CT");
        Region DC = Region.FromId("DC");
        Region DE = Region.FromId("DE");
        Region FL = Region.FromId("FL");
        Region GA = Region.FromId("GA");
        Region GU = Region.FromId("GU");
        Region HI = Region.FromId("HI");
        Region IA = Region.FromId("IA");
        Region ID = Region.FromId("ID");
        Region IL = Region.FromId("IL");
        Region IN = Region.FromId("IN");
        Region KS = Region.FromId("KS");
        Region KY = Region.FromId("KY");
        Region LA = Region.FromId("LA");
        Region MA = Region.FromId("MA");
        Region MD = Region.FromId("MD");
        Region ME = Region.FromId("ME");
        Region MI = Region.FromId("MI");
        Region MN = Region.FromId("MN");
        Region MO = Region.FromId("MO");
        Region MP = Region.FromId("MP");
        Region MS = Region.FromId("MS");
        Region MT = Region.FromId("MT");
        Region NC = Region.FromId("NC");
        Region ND = Region.FromId("ND");
        Region NE = Region.FromId("NE");
        Region NH = Region.FromId("NH");
        Region NJ = Region.FromId("NJ");
        Region NM = Region.FromId("NM");
        Region NV = Region.FromId("NV");
        Region NY = Region.FromId("NY");
        Region OH = Region.FromId("OH");
        Region OK = Region.FromId("OK");
        Region OR = Region.FromId("OR");
        Region PA = Region.FromId("PA");
        Region PR = Region.FromId("PR");
        Region RI = Region.FromId("RI");
        Region SC = Region.FromId("SC");
        Region SD = Region.FromId("SD");
        Region TN = Region.FromId("TN");
        Region TX = Region.FromId("TX");
        Region UT = Region.FromId("UT");
        Region VA = Region.FromId("VA");
        Region VI = Region.FromId("VI");
        Region VT = Region.FromId("VT");
        Region WA = Region.FromId("WA");
        Region WI = Region.FromId("WI");
        Region WV = Region.FromId("WV");
        Region WY = Region.FromId("WY");

        return new PresetMap
        {
            Regions =
            [
                AK, AL, AR, AS, AZ,
                CA, CO, CT, DC, DE,
                FL, GA, GU, HI, IA,
                ID, IL, IN, KS, KY,
                LA, MA, MD, ME, MI,
                MN, MO, MP, MS, MT,
                NC, ND, NE, NH, NJ,
                NM, NV, NY, OH, OK,
                OR, PA, PR, RI, SC,
                SD, TN, TX, UT, VA,
                VI, VT, WA, WI, WV,
                WY
            ],
            NeighbourPairs =
            [
                new NeighbourPair(AL, FL),
                new NeighbourPair(AL, GA),
                new NeighbourPair(AL, MS),
                new NeighbourPair(AL, TN),
                new NeighbourPair(AR, LA),
                new NeighbourPair(AR, MO),
                new NeighbourPair(AR, MS),
                new NeighbourPair(AR, OK),
                new NeighbourPair(AR, TN),
                new NeighbourPair(AR, TX),
                new NeighbourPair(AZ, CA),
                new NeighbourPair(AZ, NM),
                new NeighbourPair(AZ, NV),
                new NeighbourPair(AZ, UT),
                new NeighbourPair(CA, NV),
                new NeighbourPair(CA, OR),
                new NeighbourPair(CO, KS),
                new NeighbourPair(CO, NE),
                new NeighbourPair(CO, NM),
                new NeighbourPair(CO, OK),
                new NeighbourPair(CO, UT),
                new NeighbourPair(CO, WY),
                new NeighbourPair(CT, MA),
                new NeighbourPair(CT, NY),
                new NeighbourPair(CT, RI),
                new NeighbourPair(DC, MD),
                new NeighbourPair(DC, VA),
                new NeighbourPair(DE, MD),
                new NeighbourPair(DE, NJ),
                new NeighbourPair(DE, PA),
                new NeighbourPair(FL, GA),
                new NeighbourPair(GA, NC),
                new NeighbourPair(GA, SC),
                new NeighbourPair(GA, TN),
                new NeighbourPair(IA, IL),
                new NeighbourPair(IA, MN),
                new NeighbourPair(IA, MO),
                new NeighbourPair(IA, NE),
                new NeighbourPair(IA, SD),
                new NeighbourPair(IA, WI),
                new NeighbourPair(ID, MT),
                new NeighbourPair(ID, NV),
                new NeighbourPair(ID, OR),
                new NeighbourPair(ID, UT),
                new NeighbourPair(ID, WA),
                new NeighbourPair(ID, WY),
                new NeighbourPair(IL, IN),
                new NeighbourPair(IL, KY),
                new NeighbourPair(IL, MO),
                new NeighbourPair(IL, WI),
                new NeighbourPair(IN, KY),
                new NeighbourPair(IN, MI),
                new NeighbourPair(IN, OH),
                new NeighbourPair(KS, MO),
                new NeighbourPair(KS, NE),
                new NeighbourPair(KS, OK),
                new NeighbourPair(KY, MO),
                new NeighbourPair(KY, OH),
                new NeighbourPair(KY, TN),
                new NeighbourPair(KY, VA),
                new NeighbourPair(KY, WV),
                new NeighbourPair(LA, MS),
                new NeighbourPair(LA, TX),
                new NeighbourPair(MA, NH),
                new NeighbourPair(MA, NY),
                new NeighbourPair(MA, RI),
                new NeighbourPair(MA, VT),
                new NeighbourPair(MD, PA),
                new NeighbourPair(MD, VA),
                new NeighbourPair(MD, WV),
                new NeighbourPair(ME, NH),
                new NeighbourPair(MI, OH),
                new NeighbourPair(MI, WI),
                new NeighbourPair(MN, ND),
                new NeighbourPair(MN, SD),
                new NeighbourPair(MN, WI),
                new NeighbourPair(MO, NE),
                new NeighbourPair(MO, OK),
                new NeighbourPair(MO, TN),
                new NeighbourPair(MS, TN),
                new NeighbourPair(MT, ND),
                new NeighbourPair(MT, SD),
                new NeighbourPair(MT, WY),
                new NeighbourPair(NC, SC),
                new NeighbourPair(NC, TN),
                new NeighbourPair(NC, VA),
                new NeighbourPair(ND, SD),
                new NeighbourPair(NE, SD),
                new NeighbourPair(NE, WY),
                new NeighbourPair(NH, VT),
                new NeighbourPair(NJ, NY),
                new NeighbourPair(NJ, PA),
                new NeighbourPair(NM, OK),
                new NeighbourPair(NM, TX),
                new NeighbourPair(NV, OR),
                new NeighbourPair(NV, UT),
                new NeighbourPair(NY, PA),
                new NeighbourPair(NY, VT),
                new NeighbourPair(OH, PA),
                new NeighbourPair(OH, WV),
                new NeighbourPair(OK, TX),
                new NeighbourPair(OR, WA),
                new NeighbourPair(PA, WV),
                new NeighbourPair(SD, WY),
                new NeighbourPair(TN, VA),
                new NeighbourPair(UT, WY),
                new NeighbourPair(VA, WV)
            ]
        };
    }
}
