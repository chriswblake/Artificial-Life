using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Ch_7_Artificial_Life
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();

        }

        //Fields
        World theWorld;

        //Methods
        public Series ListToSeries(List<int> data, string name, string chartArea, Color lineColor)
        {
            //Create series
            Series theSeries = new Series(name) {
                ChartArea = chartArea,
                ChartType = SeriesChartType.Line,
                Color = lineColor

            };

            //Add points to series
            for (int i = 0; i < data.Count; i++)
            {
                int cycle = i + 1;
                string toolTip = "Cycle: " + i + Environment.NewLine + "Value: " + data[i].ToString("F0");
                theSeries.Points.Add(new DataPoint(cycle, data[i]) { ToolTip = toolTip });
            }

            //Return
            return theSeries;

        }
        private World simulate()
        {
            //Parameters
            int worldSize = 30;
            int percentPlants = 70;
            int percentHerbivores = 80;
            int percentCarnivores = 5;

            int energyInitHerbivores = 50;
            int energyReprHerbivores = 51;
            int energyInitCarnivores = 50;
            int energyReprCarnivores = 51;

            int simulationTime = 100;

            //Get parameters from interface
            try
            {
                //World
                percentPlants = Convert.ToInt32(tbPlantsPercent.Text);
                percentHerbivores = Convert.ToInt32(tbHerbivoresPercent.Text);
                percentCarnivores = Convert.ToInt32(tbCarnivoresPercent.Text);
                //Energy
                energyInitHerbivores = Convert.ToInt32(tbHerbivoresEnergyInit.Text);
                energyReprHerbivores = Convert.ToInt32(tbHerbivoresEnergyRepr.Text);
                energyInitCarnivores = Convert.ToInt32(tbCarnivoresEnergyInit.Text);
                energyReprCarnivores = Convert.ToInt32(tbCarnivoresEnergyRepr.Text);

                //Simulation
                simulationTime = Convert.ToInt32(tbSimulationTime.Text);
            }
            catch
            {
                return null;
            }

            //Create world
            double totalSpots = worldSize * worldSize;
            int numPlants = (int)Math.Round(totalSpots * (percentPlants / 100.0), 0);
            int numHerbivores = (int)Math.Round(totalSpots * (percentHerbivores / 100.0), 0);
            int numCarnivores = (int)Math.Round(totalSpots * (percentCarnivores / 100.0), 0);
            World theWorld = new World(worldSize, numPlants, numHerbivores, numCarnivores,
                energyInitHerbivores, energyReprHerbivores,
                energyInitCarnivores, energyReprCarnivores);

            //Simulate for x cycles 
            theWorld.simulate(simulationTime);

            //Return finished simulation
            return theWorld;
        }
        private void graphResults(World theWorld)
        {
            if (theWorld == null)
                return;

            //Chart Results
            chartTop.Series.Clear();
            //chartTop.Series.Add(ListToSeries(theWorld.stats.plantDeathsPerCycle, "Plant", chartTop.ChartAreas[0].Name));
            //chartTop.Series.Add(ListToSeries(theWorld.stats.herbivoreDeathsPerCycle, "Herbivore Deaths", chartTop.ChartAreas[0].Name));
            //chartTop.Series.Add(ListToSeries(theWorld.stats.carnivoreDeathsPerCycle, "Carnivore Deaths", chartTop.ChartAreas[0].Name));
            chartTop.Series.Add(ListToSeries(theWorld.stats.herbivoreBirthsPerCycle, "Herbivore Births", chartTop.ChartAreas[0].Name, Color.DarkBlue));
            chartTop.Series.Add(ListToSeries(theWorld.stats.carnivoreBirthsPerCycle, "Carnivore Births", chartTop.ChartAreas[0].Name, Color.IndianRed));
            chartTop.ResetAutoValues();

            chartBottom.Series.Clear();
            chartBottom.Series.Add(ListToSeries(theWorld.stats.herbivoreCountPerCycle, "Herbivore Count", chartBottom.ChartAreas[0].Name, Color.DarkBlue));
            chartBottom.Series.Add(ListToSeries(theWorld.stats.carnivoreCountPerCycle, "Carnivore Count", chartBottom.ChartAreas[0].Name, Color.IndianRed));
            if (chbShowHerbivoreAge.Checked)
                chartBottom.Series.Add(ListToSeries(theWorld.stats.herbivoreOldestPerCycle, "Herbivore Age", chartBottom.ChartAreas[0].Name, Color.MediumBlue));
            if (chbShowCarnivoreAge.Checked)
                chartBottom.Series.Add(ListToSeries(theWorld.stats.carnivoreOldestPerCycle, "Carnivore Age", chartBottom.ChartAreas[0].Name, Color.Pink));
            chartBottom.ResetAutoValues();
        }

        //Controls
        private void btnSimulate_Click(object sender, EventArgs e)
        {
            //Simulate the results
            theWorld = simulate();

            //Show results on graph
            graphResults(theWorld);
        }
        private void tbParameter_TextChanged(object sender, EventArgs e)
        {
            //Simulate the results
            theWorld = simulate();

            //Show results on graph
            graphResults(theWorld);
        }        
        private void tbParameter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
            {
                //Simulate the results
                theWorld = simulate();

                //Show results on graph
                graphResults(theWorld);
            }
        }
        private void chbShowAge_CheckedChanged(object sender, EventArgs e)
        {
            if (theWorld != null)
                graphResults(theWorld);
        }
    }

    //Classes - Main Simulation
    public class World
    {
        //Fields
        Random rand = new Random();
        Agent[,,] map;
        int mapSize;
        List<Plant> plants = new List<Plant>(); int minPlants = 0;
        List<Herbivore> herbivores = new List<Herbivore>();
        int numHerbivores; int energyInitHerbivores; int energyReprHerbivores;
        List<Carnivore> carnivores = new List<Carnivore>();
        int numCarnivores; int energyInitCarnivores; int energyReprCarnivores;

        //Fields - Statistics
        public Statistics stats = new Statistics();
        public class Statistics
        { 
            public List<int> herbivoreCountPerCycle = new List<int>();
            public List<int> carnivoreCountPerCycle = new List<int>();

            public List<int> plantDeathsPerCycle = new List<int>();
            public List<int> herbivoreDeathsPerCycle = new List<int>();
            public List<int> carnivoreDeathsPerCycle = new List<int>();

            public List<int> herbivoreBirthsPerCycle = new List<int>();
            public List<int> carnivoreBirthsPerCycle = new List<int>();

            public List<int> herbivoreOldestPerCycle = new List<int>();
            public List<int> carnivoreOldestPerCycle = new List<int>();
        }

        //Constructor
        public World(int size, int numPlants, int numHerbivores, int numCarnivores,
                int energyInitHerbivores, int energyReprHerbivores,
                int energyInitCarnivores, int energyReprCarnivores)
        {

            //Create map for agents to live
            map = new Agent[size, size, 3]; //0 plant, 1 herb, 2 carn
            mapSize = size;

            //Create plants
            addPlants(numPlants); minPlants = numPlants;

            //Create herbivores
            addHerbivores(numHerbivores, energyInitHerbivores);
            this.numHerbivores = numHerbivores;
            this.energyInitHerbivores = energyInitHerbivores;
            this.energyReprHerbivores = energyReprHerbivores;

            //Create carnivores
            addCarnivores(numCarnivores, energyReprCarnivores);
            this.numCarnivores = numCarnivores;
            this.energyInitCarnivores = energyInitCarnivores;
            this.energyReprCarnivores = energyReprCarnivores;
        }

        //Methods - Simulation
        public void simulate(int time)
        {
            for (int t = 1; t <= time; t++)
                simulate();
        }
        public void simulate()
        {
            #region 1.) Make plans
            //Herbivores
            foreach (Herbivore h in herbivores)
            { h.planAction(); }

            //Carnivores
            foreach (Carnivore c in carnivores)
            { c.planAction(); }
            #endregion

            #region 2.) Perform actions (from plans)
            //Herbivores
            foreach (Herbivore h in herbivores)
            { h.performAction(); }

            //Carnivores
            foreach (Carnivore c in carnivores)
            { c.performAction(); }
            #endregion

            #region 3.) Remove the dead
            int counter = 0;
            //Plants
            counter = 0;
            foreach(Plant a in plants.FindAll(a => a.energy <=0))
            {
                //Remove form map
                map[a.location.X, a.location.Y, a.layer] = null;

                //Remove from list
                plants.Remove(a);
                counter++;              
            }
            stats.plantDeathsPerCycle.Add(counter);

            //Herbivores
            counter = 0;
            foreach (Herbivore a in herbivores.FindAll(a => a.energy <= 0))
            {
                //Remove form map
                map[a.location.X, a.location.Y, a.layer] = null;

                //Remove from list
                herbivores.Remove(a);
                counter++;
            }
            stats.herbivoreDeathsPerCycle.Add(counter);

            //Carnivores
            counter = 0;
            foreach (Carnivore a in carnivores.FindAll(a => a.energy <= 0))
            {
                //Remove form map
                map[a.location.X, a.location.Y, a.layer] = null;

                //Remove from list
                carnivores.Remove(a);
                counter++;
            }
            stats.carnivoreDeathsPerCycle.Add(counter);

            #endregion

            #region 4.) Increase age
            foreach (Agent a in plants)
                a.age++;

            foreach (Agent a in herbivores)
                a.age++;

            foreach (Agent a in carnivores)
                a.age++;


            if (herbivores.Count > 0)
                stats.herbivoreOldestPerCycle.Add(herbivores.Max(a => a.age));
            else
                stats.herbivoreOldestPerCycle.Add(0);

            if (carnivores.Count > 0)
                stats.carnivoreOldestPerCycle.Add(carnivores.Max(a => a.age));
            else
                stats.carnivoreOldestPerCycle.Add(0);
            #endregion

            //Count herbivores and carnivores
            stats.herbivoreCountPerCycle.Add(herbivores.Count);
            stats.carnivoreCountPerCycle.Add(carnivores.Count);

            #region 5.) Reproduce
            //Herbivore
            counter = 0;        
            foreach(Herbivore h in herbivores.FindAll(a => a.energy >= energyReprHerbivores))
            {
                if (herbivores.Count < numHerbivores)
                { 
                    Herbivore child = (Herbivore) h.reproduce();
                    putAgentOnMap(child);
                    herbivores.Add(child);
                    counter++;
                }
            }
            stats.herbivoreBirthsPerCycle.Add(counter);

            //Carnivore
            counter = 0;
            foreach (Carnivore c in carnivores.FindAll(a => a.energy >= energyReprCarnivores))
            {
                if (carnivores.Count < numCarnivores)
                {
                    Carnivore child = (Carnivore)c.reproduce();
                    putAgentOnMap(child);
                    carnivores.Add(child);
                    counter++;
                }          
            }
            stats.carnivoreBirthsPerCycle.Add(counter);

            #endregion

            // 6.) Replenish, if too low
            addPlants(minPlants - plants.Count);
            if(herbivores.Count <= numHerbivores/10)
                addHerbivores(numHerbivores*9/10, energyInitHerbivores);
            if (carnivores.Count <= numCarnivores/10)
                addCarnivores(numCarnivores*9/10, energyInitCarnivores);

        }
        public Point locationMapper(Point origLoc)
        {
            //Results variable
            int x = origLoc.X;
            int y = origLoc.Y;

            //Route to opposite side
            x = (x + mapSize) % mapSize;
            y = (y + mapSize) % mapSize;
            Point newLoc = new Point(x, y);

            return newLoc;
        }

        //Methods - Support
        public Point findEmptyLocation(int layer)
        {
            while (true)
            {
                //Pick random point on map
                int x = rand.Next(0, mapSize);
                int y = rand.Next(0, mapSize);

                //Check map for agent
                if (map[x, y, layer] == null)
                    return new Point(x, y);
            }
        }
        public void addPlants(int count)
        {
            for (int a = count; a > 0; a--)
            {
                //Create an agent and add to list
                Plant theAgent = new Plant(map);
                putAgentOnMap(theAgent);
                plants.Add(theAgent);
            }
        }
        public void addHerbivores(int count, int initialEnergy)
        {
            for (int a = count; a > 0; a--)
            {
                //Create an agent and add to list
                Herbivore theAgent = new Herbivore(map, locationMapper, initialEnergy, rand);
                putAgentOnMap(theAgent);
                herbivores.Add(theAgent);
            }
        }
        public void addCarnivores(int count, int initialEnergy)
        {
            for (int a = count; a > 0; a--)
            {
                //Create an agent and add to list
                Carnivore theAgent = new Carnivore(map, locationMapper, initialEnergy, rand);
                putAgentOnMap(theAgent);
                carnivores.Add(theAgent);
            }
        }
        public void putAgentOnMap(Agent theAgent)
        {
            //Get an empty location
            Point loc = findEmptyLocation(theAgent.layer);

            //Pick random location and direction
            theAgent.location = loc;
            Array dirValues = Enum.GetValues(typeof(Agent.Direction));
            theAgent.direction = (Agent.Direction)dirValues.GetValue(rand.Next(0, dirValues.Length));

            //Put on the map
            map[loc.X, loc.Y, theAgent.layer] = theAgent;
        }
    }

    //Classes - Agents
    public class Plant : Agent
    {
        //Constructor
        public Plant(Agent[,,] map) : base(map, null, 100)
        { layer = 0; }

        //Methods
        public override void move()
        {
            //Cannot move
        }
        
        //Debug
        public override string ToString()
        {
            string info = "Plant";

            return info + " " + base.ToString();
        }
    }
    public class Herbivore : ThinkingAgent
    {
        //Constructors
        public Herbivore(Agent[,,] map, LocationMapper locationMapper, int energy, Random rand)
            : base(map, locationMapper, energy, rand)
        {
            layer = 1;
        }

        //Methods
        public override void eat()
        {
            //Find a plant in the proximity area
            Plant thePlant = findPlant();

            //Check if a plant was found
            if (thePlant == null)
            {
                energy--;
                return;
            }
            //Remove the plant from the map
            map[thePlant.location.X, thePlant.location.Y, thePlant.layer] = null;

            //Mark the plant as dead
            thePlant.energy = 0;

            //Increase energy
            energy++;

        }
        public Plant findPlant()
        {
            List<Plant> plantsInProximity = new List<Plant>();

            //Search proximity area for all plants
            for (int i = 0; i < proximity.GetLength(0); i++)
            {
                //Pick location
                int x = location.X + proximity[i, 0];
                int y = location.Y + proximity[i, 1];
                int layer = 0;

                //Transform coordinates if they are out of bounds
                Point transPoint = locationMapper(new Point(x, y));
                x = transPoint.X;
                y = transPoint.Y;

                //Count agents
                if (map[x, y, layer] == null)
                    continue;

                //Check if plant is alive
                if (map[x,y,layer].energy > 0)
                    plantsInProximity.Add((Plant)map[x, y,layer]);              
            }

            //Pick random plant from list
            if (plantsInProximity.Count > 0)
            {
                return plantsInProximity[rand.Next(0, plantsInProximity.Count)];
            }else
            {
                return null;
            }

        }

        //Debug
        public override string ToString()
        {
            string info = "Herbivore";

            return info + " " + base.ToString();
        }
    }
    public class Carnivore : ThinkingAgent
    {
        //Constructors
        public Carnivore(Agent[,,] map, LocationMapper locationMapper, int energy, Random rand)
            : base(map, locationMapper, energy, rand)
        {
            layer = 2;
        }

        //Methods
        public override void eat()
        {
            //Find an herbivore in the proximity area
            Herbivore theHerbivore = findHerbivore();

            //Check if an herbivore was found
            if (theHerbivore == null)
            {
                energy--;
                return;
            }
            //Remove the agent from the map
            map[theHerbivore.location.X, theHerbivore.location.Y, theHerbivore.layer] = null;

            //Mark the herbivore as dead
            theHerbivore.energy = 0;

            //Increase energy
            energy+=5;
        }
        public Herbivore findHerbivore()
        {
            List<Herbivore> herbivoresInProximity = new List<Herbivore>();

            //Search proximity area for all herbivores
            for (int i = 0; i < proximity.GetLength(0); i++)
            {
                //Pick location
                int x = location.X + proximity[i, 0];
                int y = location.Y + proximity[i, 1];
                int layer = 1;

                //Transform coordinates if they are out of bounds
                Point transPoint = locationMapper(new Point(x, y));
                x = transPoint.X;
                y = transPoint.Y;

                //Count agents
                if (map[x, y, layer] == null)
                    continue;

                //Check if it is a plant and alive
                if (map[x, y, layer].energy > 0)
                    herbivoresInProximity.Add((Herbivore)map[x, y, layer]);
            }

            //Pick random herbivore from list
            if (herbivoresInProximity.Count > 0)
            {
                return herbivoresInProximity[rand.Next(0, herbivoresInProximity.Count)];
            }
            else
            {
                return null;
            }

        }

        //Debug
        public override string ToString()
        {
            string info = "Carnivore";
            return info + " " + base.ToString();
        }
    }
    public class ThinkingAgent : Agent
    {
        //Fields - Thinking
        protected Random rand;
        public Brain brain;
        Brain.ActionOption plannedAction = Brain.ActionOption.Move;

        //Fields - Perception Offsets
        protected int[,] front = northFront;
        protected int[,] right = northRight;
        protected int[,] left = northLeft;
        protected int[,] proximity = northProx;

        //Constructors
        public ThinkingAgent(Agent[,,] map, LocationMapper locationMapper, int energy, Random rand)
            : base(map, locationMapper, energy)
        {
            this.rand = rand;
            brain = new Brain(rand);
            determinePerceptionOffsets();
        }

        //Methods - thinking
        #region Perception Offsets
        static int[,] northFront = { { -2, -2 }, { -1, -2 }, { 0, -2 }, { 1, -2 }, { 2, -2 } };
        static int[,] northLeft = { { -2, 0 }, { -2, 1 } };
        static int[,] northRight = { { 2, 0 }, { 2, 1 } };
        static int[,] northProx = { { 0, 0 }, { -1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 }, { 1, 0 } };

        static int[,] southFront = { { -2, 2 }, { -1, 2 }, { 0, 2 }, { 1, 2 }, { 2, 2 } };
        static int[,] southLeft = { { -2, 0 }, { -2, -1 } };
        static int[,] southRight = { { 2, 0 }, { 2, -1 } };
        static int[,] southProx = { { 0, 0 }, { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 }, { 1, 0 } };

        static int[,] westFront = { { -2, -2 }, { -2, -1 }, { -2, 0 }, { -2, 1 }, { -2, 2 } };
        static int[,] westLeft = { { 0, 2 }, { -1, 2 } };
        static int[,] westRight = { { 0, -2 }, { -1, -2 } };
        static int[,] westProx = { { 0, 0 }, { 0, 1 }, { -1, 1 }, { -1, 0 }, { -1, -1 }, { 0, -1 } };

        static int[,] eastFront = { { 2, -2 }, { 2, -1 }, { 2, 0 }, { 2, 1 }, { 2, 2 } };
        static int[,] eastLeft = { { 0, 2 }, { 1, 2 } };
        static int[,] eastRight = { { 0, -2 }, { 1, -2 } };
        static int[,] eastProx = { { 0, 0 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 } };
        #endregion
        public virtual Brain.input perceive()
        {
            //Results variable
            Brain.input results = new Brain.input();          

            #region Count for each area
            //Front
            var offset = front;
            for (int i = 0; i < offset.GetLength(0); i++)
            {
                //Pick location
                int x = location.X + offset[i, 0];
                int y = location.Y + offset[i, 1];

                //Transform coordinates if they are out of bounds
                Point transPoint = locationMapper(new Point(x, y));
                x = transPoint.X;
                y = transPoint.Y;

                //Count agents
                if (map[x, y, 0] != null)
                    results.FrontPlant++;
                else if (map[x, y, 1] != null)
                    results.FrontHerbivore++;
                else if (map[x, y, 2] != null)
                    results.FrontCarnivore++;
            }

            //Left
            offset = left;
            for (int i = 0; i < offset.GetLength(0); i++)
            {
                //Pick location
                int x = location.X + offset[i, 0];
                int y = location.Y + offset[i, 1];

                //Transform coordinates if they are out of bounds
                Point transPoint = locationMapper(new Point(x, y));
                x = transPoint.X;
                y = transPoint.Y;

                //Count agents
                if (map[x, y, 0] != null)
                    results.LeftPlant++;
                else if (map[x, y, 1] != null)
                    results.LeftHerbivore++;
                else if (map[x, y, 2] != null)
                    results.LeftCarnivore++;
            }

            //Right
            offset = right;
            for (int i = 0; i < offset.GetLength(0); i++)
            {
                //Pick location
                int x = location.X + offset[i, 0];
                int y = location.Y + offset[i, 1];

                //Transform coordinates if they are out of bounds
                Point transPoint = locationMapper(new Point(x, y));
                x = transPoint.X;
                y = transPoint.Y;

                //Count agents
                if (map[x, y, 0] != null)
                    results.RightPlant++;
                else if (map[x, y, 1] != null)
                    results.RightHerbivore++;
                else if (map[x, y, 2] != null)
                    results.RightCarnivore++;
            }

            //Proximity
            offset = proximity;
            for (int i = 0; i < offset.GetLength(0); i++)
            {
                //Pick location
                int x = location.X + offset[i, 0];
                int y = location.Y + offset[i, 1];

                //Transform coordinates if they are out of bounds
                Point transPoint = locationMapper(new Point(x, y));
                x = transPoint.X;
                y = transPoint.Y;

                //Count agents
                if (map[x, y, 0] != null)
                    results.ProximityPlant++;
                else if (map[x, y, 1] != null)
                    results.ProximityHerbivore++;
                else if (map[x, y, 2] != null)
                    results.ProximityCarnivore++;
            }
            #endregion

            //Return counts
            return results;
        }
        public void planAction()
        {
            //Look at the map and decide on an action
            plannedAction = brain.decideAction(perceive());
        }
        public void performAction()
        {
            switch(plannedAction)
            {
                case Brain.ActionOption.Move:
                    move();
                    break;
                case Brain.ActionOption.TurnLeft:
                    turnLeft();
                    break;
                case Brain.ActionOption.TurnRight:
                    turnRight();
                    break;
                case Brain.ActionOption.Eat:
                    eat();
                    break;
            }
        }

        //Methods - actions
        public override void turnLeft()
        {
            base.turnLeft();
            determinePerceptionOffsets();
        }
        public override void turnRight()
        {
            base.turnRight();
            determinePerceptionOffsets();
        }
        private void determinePerceptionOffsets()
        {
            switch (direction)
            {
                case Direction.North:
                    front = northFront;
                    left = northLeft;
                    right = northRight;
                    proximity = northProx;
                    break;

                case Direction.East:
                    front = eastFront;
                    left = eastLeft;
                    right = eastRight;
                    proximity = eastProx;
                    break;

                case Direction.South:
                    front = southFront;
                    left = southLeft;
                    right = southRight;
                    proximity = southProx;
                    break;

                case Direction.West:
                    front = westFront;
                    left = westLeft;
                    right = westRight;
                    proximity = westProx;
                    break;
            }
        }

        //Methods - misc
        public virtual ThinkingAgent reproduce()
        {
            #region 1.) Create a copy
            //Copy agent (as Herbivore or Carnivore) with half energy
            ThinkingAgent clone = (ThinkingAgent) Activator.CreateInstance(this.GetType(), map, locationMapper, energy/2, rand);
            clone.parent = this;
            clone.age = 0;
            clone.generation++;

            //Reduce parent's energy by half
            this.energy /= 2;
            #endregion

            #region 2.) Copy brain and possibly mutate
            //Parameters
            double mutationChance = 0.2;

            //Copy the brain
            clone.brain = this.brain;
            clone.rand = this.rand;

            //Mutate a random weight in the brain
            if (rand.NextDouble() <= mutationChance)            
                brain.weights[rand.Next(0, brain.weights.Length)] = rand.NextDouble();

            //Mutate a random bias in the brain
            if (rand.NextDouble() <= mutationChance)
                brain.biases[rand.Next(0, brain.biases.Length)] = rand.NextDouble();
            #endregion

            return clone;
        }

        //Classes
        public struct Brain
        {
            //Constants
            private const int numInputs = 12;
            private const int numOutputs = 4;

            //Fields
            public double[] weights;
            public double[] biases;

            //Contructor
            public Brain(Random rand)
            {
                //Initialize weights randomly
                weights = new double[numInputs];
                for (int i = 0; i < numInputs; i++)
                { weights[i] = rand.NextDouble(); }

                //Initialize biases randomly
                biases = new double[numOutputs];
                for (int i = 0; i < numOutputs; i++)
                { biases[i] = rand.NextDouble(); }
            }
            public Brain(double[] weights, double[] biases)
            {
                //Copy from previous
                this.weights = weights;
                this.biases = biases;
            }

            //Methods
            public ActionOption decideAction(input inputValues)
            {
                //1.1) Convert inputs to an array
                int[] inputs = inputValues.ToArray();

                //1.2) Placeholder for calculations
                double[] outputs = new double[numOutputs];

                #region 2.) Forward propagate inputs through weights to outputs
                for (int o = 0; o < numOutputs; o++)
                {
                    //Begin with bias
                    outputs[o] = biases[o];

                    //Add all facors to output: weights*inputs
                    for (int i = 0; i < numInputs; i++)
                    {
                        outputs[o] += weights[i] * inputs[i];
                    }
                }
                #endregion

                #region 3.) Convert outputs to ActionOption
                //Find index of max value in outputs (0 to 3)
                int indexOfMax = outputs.ToList().IndexOf(outputs.Max());

                //Map index values to actions
                switch (indexOfMax)
                {
                    case 0:
                        return ActionOption.Eat;
                    case 1:
                        return ActionOption.Move;
                    case 2:
                        return ActionOption.TurnLeft;
                    case 3:
                        return ActionOption.TurnRight;
                }

                //This should not be reachable
                return ActionOption.Move;
                #endregion
            }

            //Classes
            public struct input
            {
                //Fields
                public int FrontPlant;
                public int FrontHerbivore;
                public int FrontCarnivore;

                public int LeftPlant;
                public int LeftHerbivore;
                public int LeftCarnivore;

                public int RightPlant;
                public int RightHerbivore;
                public int RightCarnivore;

                public int ProximityPlant;
                public int ProximityHerbivore;
                public int ProximityCarnivore;

                //Properties
                public int[] ToArray()
                {
                    return new int[] {
                        FrontPlant,
                        FrontHerbivore,
                        FrontCarnivore,

                        LeftPlant,
                        LeftHerbivore,
                        LeftCarnivore,

                        RightPlant,
                        RightHerbivore,
                        RightCarnivore,

                        ProximityPlant,
                        ProximityHerbivore,
                        ProximityCarnivore

                    };
                }

                //Operations
                public static input operator +(input a, input b)
                {
                    return new input()
                    {
                        FrontPlant = a.FrontPlant + b.FrontPlant,
                        FrontHerbivore = a.FrontHerbivore + b.FrontHerbivore,
                        FrontCarnivore = a.FrontCarnivore + b.FrontCarnivore,

                        LeftPlant = a.LeftPlant + b.LeftPlant,
                        LeftHerbivore = a.LeftHerbivore + b.LeftHerbivore,
                        LeftCarnivore = a.LeftCarnivore + b.LeftCarnivore,

                        RightPlant = a.RightPlant + b.RightPlant,
                        RightHerbivore = a.RightHerbivore + b.RightHerbivore,
                        RightCarnivore = a.RightCarnivore + b.RightCarnivore,

                        ProximityPlant = a.ProximityPlant + b.ProximityPlant,
                        ProximityHerbivore = a.ProximityHerbivore + b.ProximityHerbivore,
                        ProximityCarnivore = a.ProximityCarnivore + b.ProximityCarnivore
                    };
                }
                public static input operator -(input a, input b)
                {
                    return new input()
                    {
                        FrontPlant = a.FrontPlant - b.FrontPlant,
                        FrontHerbivore = a.FrontHerbivore - b.FrontHerbivore,
                        FrontCarnivore = a.FrontCarnivore - b.FrontCarnivore,

                        LeftPlant = a.LeftPlant - b.LeftPlant,
                        LeftHerbivore = a.LeftHerbivore - b.LeftHerbivore,
                        LeftCarnivore = a.LeftCarnivore - b.LeftCarnivore,

                        RightPlant = a.RightPlant - b.RightPlant,
                        RightHerbivore = a.RightHerbivore - b.RightHerbivore,
                        RightCarnivore = a.RightCarnivore - b.RightCarnivore,

                        ProximityPlant = a.ProximityPlant - b.ProximityPlant,
                        ProximityHerbivore = a.ProximityHerbivore - b.ProximityHerbivore,
                        ProximityCarnivore = a.ProximityCarnivore - b.ProximityCarnivore
                    };
                }
            }
            public enum ActionOption
            {
                TurnLeft,
                TurnRight,
                Move,
                Eat
            }
        }

        //Debug
        public override string ToString()
        {
            string decision = plannedAction.ToString();
            return base.ToString() + " (Act:" + decision.ToString().PadRight(9) + ")";
        }
    }
    public class Agent
    {
        //Constants
        public const int maxEnergy = 100;

        //Fields - Life
        public int energy;
        public Agent parent = null;
        public int age;
        public int generation;

        //Fields - location
        public Agent[,,] map; 
        public Point location = new Point(); public int layer;
        public Direction direction;
        public LocationMapper locationMapper;

        //Constructors
        public Agent(Agent[,,] map, LocationMapper locationMapper, int energy)
        {
            this.age = 0;
            this.map = map;
            this.locationMapper = locationMapper;
            this.energy = energy;
        }

        //Methods
        public virtual void move()
        {
            //Reduce energy
            energy--;

            //Create new location based on direction
            Point newLocation = new Point(location.X, location.Y);
            switch(direction)
            {
                case (Direction.North):
                    newLocation.Y--;
                    break;
                case (Direction.East):
                    newLocation.X++;
                    break;
                case (Direction.South):
                    newLocation.Y++;
                    break;
                case (Direction.West):
                    newLocation.X--;
                    break;
            }

            //Map new location to map
            newLocation = locationMapper(newLocation);

            //Check if movement is possible
            if (map[newLocation.X, newLocation.Y, layer] != null)
                return; //something is already there

            //Update position on map
            map[newLocation.X, newLocation.Y, layer] = map[location.X, location.Y, layer];
            map[location.X, location.Y, layer] = null;

            //Update location in agent
            location.X = newLocation.X;
            location.Y = newLocation.Y;
        }
        public virtual void eat() { }
        public virtual void turnLeft()
        {
            //Reduce energy
            energy--;

            switch (direction)
            {
                case (Direction.North):
                    direction = Direction.West;
                    break;
                case (Direction.East):
                    direction = Direction.North;
                    break;
                case (Direction.South):
                    direction = Direction.East;
                    break;
                case (Direction.West):
                    direction = Direction.South;
                    break;
            }
        }
        public virtual void turnRight()
        {
            //Reduce energy
            energy--;

            switch (direction)
            {
                case (Direction.North):
                    direction = Direction.East;
                    break;
                case (Direction.East):
                    direction = Direction.South;
                    break;
                case (Direction.South):
                    direction = Direction.West;
                    break;
                case (Direction.West):
                    direction = Direction.North;
                    break;
            }

            energy--;
        }

        //Classes
        public enum Direction
        {
            North,
            East,
            South,
            West
        }
        public delegate Point LocationMapper(Point loc);

        //Debug
        public override string ToString()
        {
            string info = "";
            info += "(Energy:" + energy + ")";
            info += " (Loc:" + location.X.ToString().PadLeft(2) + "," + location.Y.ToString().PadLeft(2) + " " + direction.ToString().PadRight(5) + ")";
            info += " (Age:" + age + ")";
            info += " (Gen:" + generation + ")";


            return info;
        }
    }
}
