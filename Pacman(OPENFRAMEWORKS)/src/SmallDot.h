#ifndef SMALLDOT_H
#define SMALLDOT_H

#include "ofMain.h"
#include "Utils.h"
#include "Entity.h"

class SmallDot : public Entity{
	public:
		SmallDot();
		~SmallDot();

		void init(int x, int y);
		void render();
		Classes getClassName() { return SMALLDOT; };

	protected:
	
};

#endif
