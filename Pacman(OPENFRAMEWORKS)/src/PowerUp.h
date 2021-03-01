#ifndef POWERUP_H
#define POWERUP_H

#include "ofMain.h"
#include "Utils.h"
#include "Entity.h"
class PowerUp : public Entity{
	public:
		PowerUp();
		~PowerUp();
	
		void render();
		void init(int x, int y);
		Classes getClassName() { return POWERUP; };

	protected:
	
};

#endif
