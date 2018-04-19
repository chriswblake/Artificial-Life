typedef struct {
	short type;
	short energy;
	short parent;
	short age;
	short generation;
	locType location;
	unsigned short direction;
	short inputs[MAX_INPUTS];
	short weight_oi[MAX_INPUTS * MAX_OUTPUTS];
	short biaso[MAX_OUTPUTS];
	short actions[MAX_OUTPUTS];
} agentType;

#define TYPE_HERBIVORE  0
#define TYPE_CARNIVORE  1
#define TYPE_DEAD       -1

typedef struct {
	short x;
	short y;
} locType;


#define HERB_FRONT      0
#define CARN_FRONT      1
#define PLANT_FRONT     2
#define HERB_LEFT       3
#define CARN_LEFT       4
#define PLANT_LEFT      5
#define HERB_RIGHT      6
#define CARN_RIGHT      7
#define PLANT_RIGHT     8
#define HERB_PROXIMITY  9
#define CARN_PROXIMITY  10
#define PLANT_PROXIMITY 11
#define MAX_INPUTS      12


#define ACTION_TURN_LEFT        0
#define ACTION_TURN_RIGHT       1
#define ACTION_MOVE             2
#define ACTION_EAT              3

#define MAX_OUTPUTS     4


#define HERB_PLANE        0
#define CARN_PLANE        1
#define PLANT_PLANE       2

#define MAX_GRID   30

//Agent environment in 3 dimensions (to have independent planes for plant, herbivore and carnivore objects.
int landscape[3][MAX_GRID][MAX_GRID];

#define MAX_AGENTS 36
#define MAX_PLANTS 30

agentType agents[MAX_AGENTS];
int agentCount = 0;

plantType plants[MAX_PLANTS];
int plantCount = 0;


#define getSRand()      ((float)rand() / (float)RAND_MAX)
#define getRand(x)      (int)((x) * getSRand())
#define getWeight()     (getRand(9)-1)

int main(int argc, char *argv[])
{
	int i;

	/* Seed the random number generator */
	srand(time(NULL));

	/* Initialize the simulation */
	init();

	/* Main loop for the simulation. */
	for (i = 0; i < MAX_STEPS; i++) {

		/* Simulate each agent for one time step */
		simulate();
	}

	return 0;
}
void init(void)
{

	/* Initialize the landscape */
	bzero((void *)landscape, sizeof(landscape));

	bzero((void *)bestAgent, sizeof(bestAgent));

	/* Initialize the plant plane */
	for (plantCount = 0; plantCount < MAX_PLANTS; plantCount++) {
		growPlant(plantCount);
	}

	/* Randomly initialize the Agents */
	for (agentCount = 0; agentCount < MAX_AGENTS; agentCount++) {

		if (agentCount < (MAX_AGENTS / 2)) {
			agents[agentCount].type = TYPE_HERBIVORE;
		}
		else {
			agents[agentCount].type = TYPE_CARNIVORE;
		}

		initAgent(&agents[agentCount]);

	}

}
void growPlant(int i)
{
	int x, y;

	while (1) {

		/* Pick a random location in the environment */
		x = getRand(MAX_GRID); y = getRand(MAX_GRID);

		/* As long as a plant isn't already there */
		if (landscape[PLANT_PLANE][y][x] == 0) {

			/* Update the environment for the new plant */
			plants[i].location.x = x;
			plants[i].location.y = y;
			landscape[PLANT_PLANE][y][x]++;
			break;

		}

	}

	return;
}
void initAgent(agentType *agent)
{
	int i;

	agent->energy = (MAX_ENERGY / 2);
	agent->age = 0;
	agent->generation = 1;

	agentTypeCounts[agent->type]++;

	findEmptySpot(agent);

	for (i = 0; i < (MAX_INPUTS * MAX_OUTPUTS); i++) {
		agent->weight_oi[i] = getWeight();
	}

	for (i = 0; i < MAX_OUTPUTS; i++) {
		agent->biaso[i] = getWeight();
	}

	return;
}
void findEmptySpot(agentType *agent)
{
	agent->location.x = -1;
	agent->location.y = -1;

	while (1) {

		/* Pick a random location for the agent */
		agent->location.x = getRand(MAX_GRID);
		agent->location.y = getRand(MAX_GRID);

		/* If an agent isn't there already, break out of the loop */
		if (landscape[agent->type]
			[agent->location.y][agent->location.x] == 0)
			break;

	}

	/* Pick a random direction for the agent, and update the map */
	agent->direction = getRand(MAX_DIRECTION);
	landscape[agent->type][agent->location.y][agent->location.x]++;

	return;
}
void simulate(void)
{
	int i, type;

	/* Simulate the herbivores first, then the carnivores */
	for (type = TYPE_HERBIVORE; type <= TYPE_CARNIVORE; type++) {

		for (i = 0; i < MAX_AGENTS; i++) {

			if (agents[i].type == type) {

				simulateAgent(&agents[i]);

			}

		}

	}

}

const offsetPairType northFront[] = { { -2,-2 },{ -2,-1 },{ -2,0 },{ -2,1 },{ -2,2 },{ 9,9 } };
const offsetPairType northLeft[]  = { { 0,-2 },{ -1,-2 },{ 9,9 } };
const offsetPairType northRight[] = { { 0,2 },{ -1,2 },{ 9,9 } };
const offsetPairType northProx[]  = { { 0,-1 },{ -1,-1 },{ -1,0 },{ -1,1 },{ 0,1 },{ 9,9 } };

const offsetPairType westFront[] = { { 2,-2 },{ 1,-2 },{ 0,-2 },{ -1,-2 },{ -2,-2 },{ 9,9 } };
const offsetPairType westLeft[]  = { { 2,0 },{ 2,-1 },{ 9,9 } };
const offsetPairType westRight[] = { { -2,0 },{ -2,-1 },{ 9,9 } };
const offsetPairType westProx[]  = { { 1,0 },{ 1,-1 },{ 0,-1 },{ -1,-1 },{ -1,0 },{ 9,9 } };

void simulateAgent(agentType *agent)
{
	int x, y;
	int out, in;
	int largest, winner;

	/* Use shorter names. */
	x = agent->location.x;
	y = agent->location.y;

	/* Determine inputs for the agent neural network */
	switch (agent->direction)
	{
		case NORTH:
			percept(x, y, &agent->inputs[HERB_FRONT], northFront, 1);
			percept(x, y, &agent->inputs[HERB_LEFT], northLeft, 1);
			percept(x, y, &agent->inputs[HERB_RIGHT], northRight, 1);
			percept(x, y, &agent->inputs[HERB_PROXIMITY], northProx, 1);
			break;

		case SOUTH:
			percept(x, y, &agent->inputs[HERB_FRONT], northFront, -1);
			percept(x, y, &agent->inputs[HERB_LEFT], northLeft, -1);
			percept(x, y, &agent->inputs[HERB_RIGHT], northRight, -1);
			percept(x, y, &agent->inputs[HERB_PROXIMITY], northProx, -1
			);
			break;

		case WEST:
			percept(x, y, &agent->inputs[HERB_FRONT], westFront, 1);
			percept(x, y, &agent->inputs[HERB_LEFT], westLeft, 1);
			percept(x, y, &agent->inputs[HERB_RIGHT], westRight, 1);
			percept(x, y, &agent->inputs[HERB_PROXIMITY], westProx, 1);
			break;

		case EAST:
			percept(x, y, &agent->inputs[HERB_FRONT], westFront, -1);
			percept(x, y, &agent->inputs[HERB_LEFT], westLeft, -1);
			percept(x, y, &agent->inputs[HERB_RIGHT], westRight, -1);
			percept(x, y, &agent->inputs[HERB_PROXIMITY], westProx, -1);
			break;
	}

	/* Forward propogate the inputs through the neural network */
	for (out = 0; out < MAX_OUTPUTS; out++) {

		/* Initialize the output node with the bias */
		agent->actions[out] = agent->biaso[out];

		/* Multiply the inputs by the weights for this output node */
		for (in = 0; in < MAX_INPUTS; in++)
		{
			agent->actions[out] += (agent->inputs[in] * agent->weight_oi[(out * MAX_INPUTS) + in]);
		}

	}

	largest = -9;
	winner = -1;

	/* Select the largest node (winner-takes-all network) */
	for (out = 0; out < MAX_OUTPUTS; out++) {
		if (agent->actions[out] >= largest) {
			largest = agent->actions[out];
			winner = out;
		}
	}

	/* Perform Action */
	switch (winner) {

	case ACTION_TURN_LEFT:
	case ACTION_TURN_RIGHT:
		turn(winner, agent);
		break;

	case ACTION_MOVE:
		move(agent);
		break;

	case ACTION_EAT:
		eat(agent);
		break;

	}

	//Consume some amount of energy.
	//Herbivores, in this simulation, require more energy to survive than carnivores.
	if (agent->type == TYPE_HERBIVORE) {
		agent->energy -= 2;
	}
	else {
		agent->energy -= 1;
	}

	//If energy falls to or below zero, the agent dies. Otherwise,
	//we check to see if the agent has lived longer than any other
	// agent of the particular type.
	if (agent->energy <= 0) {
		killAgent(agent);
	}
	else {
		agent->age++;
		if (agent->age > agentMaxAge[agent->type]) {
			agentMaxAge[agent->type] = agent->age;
			agentMaxPtr[agent->type] = agent;
		}
	}

	return;
}
void percept(int x, int y, short *inputs, const offsetPairType *offsets, int neg)
{
	int plane, i;
	int xoff, yoff;

	//Work through each of the planes in the environment
	for (plane = HERB_PLANE; plane <= PLANT_PLANE; plane++) {

		//Initialize the inputs
		inputs[plane] = 0;
		i = 0;

		//Continue until we've reached the end of the offsets
		while (offsets[i].x_offset != 9) {

			//Compute the actual x and y offsets for the current position.
			xoff = x + (offsets[i].x_offset * neg);
			yoff = y + (offsets[i].y_offset * neg);

			// Clip the offsets (force the toroid as shown by Figure 7.2.
			xoff = clip(xoff);
			yoff = clip(yoff);

			// If something is in the plane, count it
			if (landscape[plane][yoff][xoff] != 0) {
				inputs[plane]++;
			}

			i++;

		}
	}

	return;
}
int clip(int z)
{
	if (z > MAX_GRID - 1) z = (z % MAX_GRID);
	else if (z < 0) z = (MAX_GRID + z);
	return z;
}
void turn(int action, agentType *agent)
{
	/* Since our agent can turn only left or right, we determine
	* the new direction based upon the current direction and the
	* turn action.
	*/
	switch (agent->direction) {

	case NORTH:
		if (action == ACTION_TURN_LEFT) agent->direction = WEST;
		else agent->direction = EAST;
		break;

	case SOUTH:
		if (action == ACTION_TURN_LEFT) agent->direction = EAST;
		else agent->direction = WEST;
		break;

	case EAST:
		if (action == ACTION_TURN_LEFT) agent->direction = NORTH;
		else agent->direction = SOUTH;
		break;

	case WEST:
		if (action == ACTION_TURN_LEFT) agent->direction = SOUTH;
		else agent->direction = NORTH;
		break;

	}

	return;
}
void move(agentType *agent)
{
	/* Determine new position offset based upon current direction */
	const offsetPairType offsets[4] = { { -1,0 },{ 1,0 },{ 0,1 },{ 0,-1 } };

	/* Remove the agent from the landscape. */
	landscape[agent->type][agent->location.y][agent->location.x]--;

	//Update the agent's X,Y position (including clipping)
	agent->location.x = clip(agent->location.x + offsets[agent->direction].x_offset);
	agent->location.y = clip(agent->location.y + offsets[agent->direction].y_offset );

	/* Add the agent back onto the landscape */
	landscape[agent->type][agent->location.y][agent->location.x]++;

	return;
}
void eat(agentType *agent)
{
	int plane, ax, ay, ox, oy, ret = 0;

	/* First, determine the plane that we'll eat from based upon
	* our agent type (carnivores eat herbivores, herbivores eat
	* plants).
	*/
	if (agent->type == TYPE_CARNIVORE) plane = HERB_PLANE;
	else if (agent->type == TYPE_HERBIVORE) plane = PLANT_PLANE;

	/* Use shorter location names */
	ax = agent->location.x;
	ay = agent->location.y;

	/* Choose the object to consume based upon direction (in the
	* proximity of the agent).
	*/
	switch (agent->direction) {

	case NORTH:
		ret = chooseObject(plane, ax, ay, northProx, 1, &ox, &oy);
		break;

	case SOUTH:
		ret = chooseObject(plane, ax, ay, northProx, -1, &ox, &oy);
		break;

	case WEST:
		ret = chooseObject(plane, ax, ay, westProx, 1, &ox, &oy);
		break;

	case EAST:
		ret = chooseObject(plane, ax, ay, westProx, -1, &ox, &oy);
		break;

	}

	/* Found an object -- eat it! */
	if (ret) {

		int i;

		if (plane == PLANT_PLANE) {

			/* Find the plant in the plant list (based upon position) */
			for (i = 0; i < MAX_PLANTS; i++) {
				if ((plants[i].location.x == ox) &&
					(plants[i].location.y == oy))
					break;
			}

			/* If found, remove it and grow a new plant elsewhere */
			if (i < MAX_PLANTS) {
				agent->energy += MAX_FOOD_ENERGY;
				if (agent->energy > MAX_ENERGY) agent->energy =
					MAX_ENERGY;
				landscape[PLANT_PLANE][oy][ox]--;
				growPlant(i);
			}

		}
		else if (plane == HERB_PLANE) {

			/* Find the herbivore in the list of agents (based upon
			* position).
			*/
			for (i = 0; i < MAX_AGENTS; i++) {
				if ((agents[i].location.x == ox) &&
					(agents[i].location.y == oy))
					break;
			}

			/* If found, remove the agent from the simulation */
			if (i < MAX_AGENTS) {
				agent->energy += (MAX_FOOD_ENERGY * 2);
				if (agent->energy > MAX_ENERGY) agent->energy =
					MAX_ENERGY;
				killAgent(&agents[i]);
			}

		}

		/* If our agent has reached the energy level to reproduce,
		allow
		* it to do so (as long as the simulation permits it).
		*/
		if (agent->energy > (REPRODUCE_ENERGY * MAX_ENERGY)) {
			if (noRepro == 0) {
				reproduceAgent(agent);
				agentBirths[agent->type]++;
			}
		}

	}

	return;

}
int chooseObject(int plane, int ax, int ay,	const offsetPairType *offsets, int neg, int *ox, int *oy)
{
	int xoff, yoff, i = 0;

	//Work through each of the offset pairs
	while (offsets[i].x_offset != 9) {

		//Determine next x,y offset
		xoff = ax + (offsets[i].x_offset * neg);
		yoff = ay + (offsets[i].y_offset * neg);

		xoff = clip(xoff);
		yoff = clip(yoff);

		//If an object is found at the check position, return the indices.
		if (landscape[plane][yoff][xoff] != 0) {
			*ox = xoff; *oy = yoff;
			return 1;
		}

		//Check the next offset
		i++;

	}

	return 0;
}
void killAgent(agentType *agent)
{
	agentDeaths[agent->type]++;

	/* Death came to this agent (or it was eaten)... */
	landscape[agent->type][agent->location.y][agent->location.x]--;
	agentTypeCounts[agent->type]--;

	if (agent->age > bestAgent[agent->type].age) {
		memcpy((void *)&bestAgent[agent->type],
			(void *)agent, sizeof(agentType));
	}

	/* 50% of the agent spots are reserved for asexual reproduction.
	* If we fall under this, we create a new random agent.
	*/
	if (agentTypeCounts[agent->type] < (MAX_AGENTS / 4)) {

		/* Create a new agent */
		initAgent(agent);

	}
	else {

		agent->location.x = -1;
		agent->location.y = -1;
		agent->type = TYPE_DEAD;

	}

	return;
}
void reproduceAgent(agentType *agent)
{
	agentType *child;
	int i;

	//Don't allow an agent type to occupy more than half of the available agent slots.
	if (agentTypeCounts[agent->type] < (MAX_AGENTS / 2)) {
		// Find an empty spot and copy the agent, mutating one of the weights or biases.
		for (i = 0; i < MAX_AGENTS; i++) {
			if (agents[i].type == TYPE_DEAD) break;
		}

		if (i < MAX_AGENTS) {

			child = &agents[i];

			memcpy((void *)child, (void *)agent, sizeof(agentType));

			findEmptySpot(child);

			if (getSRand() <= 0.2) {
				child->weight_oi[getRand(TOTAL_WEIGHTS)] = getWeight();
			}

			child->generation = child->generation + 1;
			child->age = 0;

			if (agentMaxGen[child->type] < child->generation) {
				agentMaxGen[child->type] = child->generation;
			}

			// Reproducing halves the parent's energy
			child->energy = agent->energy = (MAX_ENERGY / 2);

			agentTypeCounts[child->type]++;
			agentTypeReproductions[child->type]++;

		}

	}

	return;
}