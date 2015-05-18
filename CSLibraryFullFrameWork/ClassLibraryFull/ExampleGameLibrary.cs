using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_GOL_LibraryFull
{
    public class ExampleGameLibrary
    {
        public static Dictionary<String, SavedGame> ExampleGames = new Dictionary<String, SavedGame>(3)
        { 
        {"72DCAC0B-463D-4196-B219-A2ED3A985C71",  new SavedGame(){
               Title="Gun Glider Goose",
               Description="Gun Glider Goose see wiki page", 
               SavedDatedTime=DateTime.Parse("May 18, 2015"),
               Rows=11,
               Columns=37,
               InitialAlivePositions= new List<GameGridPosition>(){
                   //left square
                   new GameGridPosition(5,1),
                   new GameGridPosition(6,1),
                   new GameGridPosition(5,2),
                   new GameGridPosition(6,2),
                   
                   // goose face side view
                   new GameGridPosition(5,11),
                   new GameGridPosition(6,11),
                   new GameGridPosition(7,11),
                   new GameGridPosition(4,12),
                   new GameGridPosition(8,12),
                   new GameGridPosition(3,13),
                   new GameGridPosition(9,13),
                   new GameGridPosition(3,14),
                   new GameGridPosition(9,14),
                   new GameGridPosition(6,15),
                   new GameGridPosition(4,16),
                   new GameGridPosition(8,16),
                   new GameGridPosition(5,17),
                   new GameGridPosition(6,17),
                   new GameGridPosition(7,17),
                   new GameGridPosition(6,18),
                   
                   // goose face top view
                   new GameGridPosition(3,21),
                   new GameGridPosition(4,21),
                   new GameGridPosition(5,21),
                   new GameGridPosition(3,22),
                   new GameGridPosition(4,22),
                   new GameGridPosition(5,22),
                   new GameGridPosition(2,23), 
                   new GameGridPosition(6,23),
                   new GameGridPosition(2,25), 
                   new GameGridPosition(6,25),
                   new GameGridPosition(1,25), 
                   new GameGridPosition(7,25),

                   //right square
                   new GameGridPosition(3,35),
                   new GameGridPosition(4,35),
                   new GameGridPosition(3,36),
                   new GameGridPosition(4,36)

              },
               EndState= GameStateEnum.Special_Glider_Gun, 
               EndGeneration=4
        } },
         {"D72C447E-9947-40D4-BF9F-0D8FB66C01A9", new SavedGame(){
               Title="Gun Glider 1",
               Description="Gun Glider 1 see http://www.bing.com/images/search?q=game+of+life+gliders&view=detailv2&qpvt=game+of+life+gliders&id=C24C1C864713193E7A5590748A97AA71663EAFD1&selectedindex=12&ccid=aleI%2F8IA&simid=608019502552778677&thid=JN.TuiJ%2F2b7yS%2F%2BE%2BHY86pk%2FA&mode=overlay&first=1", 
               SavedDatedTime=DateTime.Parse("May 18, 2015"),
               Rows=6,
               Columns=6,
               InitialAlivePositions= new List<GameGridPosition>(){
               new GameGridPosition(1,2),
               new GameGridPosition(2,3),
               new GameGridPosition(3,1),
               new GameGridPosition(3,2),
               new GameGridPosition(3,3)
               },
               EndState= GameStateEnum.Special_Glider_Gun, 
               EndGeneration=4
            } }, 
           {"6E4DA259-E32F-4409-A6AF-9A93A7BB17DE", new SavedGame(){
               Title="Gun Glider 2",
               Description="Gun Glider 2 see http://www.bing.com/images/search?q=game+of+life+gliders&view=detailv2&qpvt=game+of+life+gliders&id=C24C1C864713193E7A5590748A97AA71663EAFD1&selectedindex=12&ccid=aleI%2F8IA&simid=608019502552778677&thid=JN.TuiJ%2F2b7yS%2F%2BE%2BHY86pk%2FA&mode=overlay&first=1", 
               SavedDatedTime=DateTime.Parse("May 18, 2015"),
               Rows=6,
               Columns=6,
               InitialAlivePositions= new List<GameGridPosition>(){
               new GameGridPosition(2,1),
               new GameGridPosition(2,3),
               new GameGridPosition(3,2),
               new GameGridPosition(3,3),
               new GameGridPosition(4,2)
               },
               EndState= GameStateEnum.Special_Glider_Gun, 
               EndGeneration=4
            } },
            {"EFAE4129-834B-4C4F-83EC-FF1C67362897", new SavedGame(){
               Title="Gun Glider 3",
               Description="Gun Glider 3 see http://www.bing.com/images/search?q=game+of+life+gliders&view=detailv2&qpvt=game+of+life+gliders&id=C24C1C864713193E7A5590748A97AA71663EAFD1&selectedindex=12&ccid=aleI%2F8IA&simid=608019502552778677&thid=JN.TuiJ%2F2b7yS%2F%2BE%2BHY86pk%2FA&mode=overlay&first=1", 
               SavedDatedTime=DateTime.Parse("May 18, 2015"),
               Rows=6,
               Columns=6,
               InitialAlivePositions= new List<GameGridPosition>(){
               new GameGridPosition(2,3),
               new GameGridPosition(3,1),
               new GameGridPosition(3,3),
               new GameGridPosition(4,3),
               new GameGridPosition(4,3)
               },
               EndState= GameStateEnum.Special_Glider_Gun, 
               EndGeneration=4
            } }, 
            {"DF91F8EE-9C2E-4751-A055-3ACC4BB27F5D", new SavedGame(){
               Title="Gun Glider 4",
               Description="Gun Glider 4 see http://www.bing.com/images/search?q=game+of+life+gliders&view=detailv2&qpvt=game+of+life+gliders&id=C24C1C864713193E7A5590748A97AA71663EAFD1&selectedindex=12&ccid=aleI%2F8IA&simid=608019502552778677&thid=JN.TuiJ%2F2b7yS%2F%2BE%2BHY86pk%2FA&mode=overlay&first=1", 
               SavedDatedTime=DateTime.Parse("May 18, 2015"),
               Rows=6,
               Columns=6,
               InitialAlivePositions= new List<GameGridPosition>(){
               new GameGridPosition(2,2),
               new GameGridPosition(3,3),
               new GameGridPosition(3,4),
               new GameGridPosition(4,2),
               new GameGridPosition(4,3)
               },
               EndState= GameStateEnum.Special_Glider_Gun, 
               EndGeneration=4
            } } 
        }
      ;
    }
}
