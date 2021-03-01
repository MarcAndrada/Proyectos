#ifndef PACMAN_H
#define PACMAN_H

#include "ofMain.h"
#include "Utils.h"
#include "Entity.h"


class Pacman : public Entity {
public:
	 Pacman ();
	~ Pacman ();

	void render();
	Classes getClassName() { return PACMAN; };

protected:
	Directions getNextDirection();
	
};

#endif