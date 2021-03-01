#include "Pacman.h"
Pacman::Pacman() : Entity() {
	PUTime = 0;
}

Pacman ::~Pacman(){
}

void Pacman::render(){
	if (Entity::mpAlive){
		if (state) {
			ofSetColor(61, 114, 190);
			PUTime += global_delta_time;
			if (PUTime > 4500){
				state = false;
				PUTime = 0;
				mpSpeed = 150;
			}
		}else {
			ofSetColor(255, 255, 0);
		}	
		Entity::render();
	}
}
Directions Pacman::getNextDirection()
{
	Directions dir = NONE;

	if (key_down['a']) {
		dir = LEFT;
	}
	else if (key_down['d']) {
		dir = RIGHT;
	}
	else if (key_down['w']) {
		dir = UP;
	}
	else if (key_down['s']) {
		dir = DOWN;
	}

	return dir;
}

