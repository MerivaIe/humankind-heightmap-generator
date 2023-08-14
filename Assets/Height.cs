using System.Collections;
using System.Collections.Generic;

public static class Height {
    public static Dictionary<int,string> hexColours;
    public static Dictionary<int,string> landTypes;

    static Height() {
        hexColours = new Dictionary<int,string>(){
            {11, "#FFFFFF"},
            {10, "#EEEEEE"},
            {9, "#DDDDDD"},
            {8, "#CCCCCC"},
            {7, "#BBBBBB"},
            {6, "#AAAAAA"},
            {5, "#999999"},
            {4, "#888888"},
            {3, "#777777"},
            {2, "#666666"},
            {1, "#555555"},
            {0, "#444444"},
            {-1, "#333333"},
            {-2, "#222222"},
            {-3, "#111111"},
            {-4, "#000000"}
        };
        landTypes = new Dictionary<int,string>(){
            {11, "Land"},
            {10, "Land"},
            {9, "Land"},
            {8, "Land"},
            {7, "Land"},
            {6, "Land"},
            {5, "Land"},
            {4, "Land"},
            {3, "Land"},
            {2, "Land"},
            {1, "Land"},
            {0, "Land"},
            {-1, "Coastal"},
            {-2, "Ocean"},
            {-3, "Ocean"},
            {-4, "Ocean"}
        };
    }
}
/*
public enum LandType {
    "Land", "Coastal", "Ocean"
}

public static class HeightMetrics {
    //May just want to use integer indexes on the Dictionaries instead of the Height enum

    //public static Dictionary<Height,HeightColour> heightColours();
    public static Dictionary<HeightTest,LandType> heightLandTypes;
    
    public static HeightMetrics{
        heightLandTypes = new Dictionary<HeightTest,LandType>(){
            {(HeightTest)11,LandType.Land},
            {(HeightTest)10,LandType.Land},
            {(HeightTest)9,LandType.Land},
            {(HeightTest)8,LandType.Land},
            {(HeightTest)7,LandType.Land},
            {(HeightTest)6,LandType.Land},
            {(HeightTest)5,LandType.Land},
            {(HeightTest)4,LandType.Land},
            {(HeightTest)3,LandType.Land},
            {(HeightTest)2,LandType.Land},
            {(HeightTest)1,LandType.Land},
            {(HeightTest)0,LandType.Land},
            {(HeightTest)-1,LandType.Coastal},
            {(HeightTest)-2,LandType.Ocean},
            {(HeightTest)-3,LandType.Ocean}
            //{(HeightTest)-4,LandType.Ocean}
        };
    }
}
*/
