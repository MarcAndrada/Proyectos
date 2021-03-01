#include "PowerUp.h"

PowerUp::PowerUp()
{
	mpRect = C_Rectangle{ 0,0,TILE_SIZE / 2,TILE_SIZE / 2 };

}

PowerUp::~PowerUp()
{

}

void PowerUp::init(int x, int y)
{
	mpRect.x = x + 1 * TILE_SIZE / 4;
	mpRect.y = y + 1 * TILE_SIZE / 4;
}

void PowerUp::render()
{
	if (Entity::mpAlive) {
		ofSetColor(255, 255, 255);
		Entity::render();
	}
}
