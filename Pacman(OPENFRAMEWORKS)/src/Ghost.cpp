#include "Ghost.h"

Ghost::Ghost(){
	srand(time(NULL));
	PUTime = 0;

}

Ghost::~Ghost(){

}

void Ghost::render() {

	if (Entity::mpAlive)
	{
		if (state) {
			ofSetColor(46, 24, 206);
			PUTime += global_delta_time;
			if (PUTime > 4500) {
				state = false;
				PUTime = 0;
				mpSpeed = currentSpeed;
			}
		}else {
			ofSetColor(255, 0, 0);
		}

		Entity::render();
	}
}



Directions Ghost::getNextDirection(){
	Directions dir = (Directions)(rand() % 4 + 1);
	return dir;

}


/*void Ghost::controlDirection(){
	
	if (!mpMoving) {
		//mpDirection = NONE;
		mpXtoGo = mpRect.x;
		mpYtoGo = mpRect.y;

		mpDirection = rand() % 4 + 1;

		/*if (mpDirection != NONE) {
			mpMoving = true;
		}

		switch (mpDirection) {
		default:
			break;
		case UP:
			mpYtoGo -= TILE_SIZE;
			break;
		case DOWN:
			mpYtoGo += TILE_SIZE;
			break;
		case LEFT:
			mpXtoGo -= TILE_SIZE;
			break;
		case RIGHT:
			mpXtoGo += TILE_SIZE;
			break;
		}
	}
}




*/