#pragma once
#ifndef GHOST_H
#define GHOST_H

#include "ofMain.h"
#include "Utils.h"
#include "Entity.h"


class Ghost : public Entity 
{
	public:
		Ghost();	
		~Ghost();

		void render();
		Classes getClassName() { return GHOST; };
		

	protected:
		
		Directions getNextDirection();
};

#endif