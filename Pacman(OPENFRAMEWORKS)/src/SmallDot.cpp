#include "SmallDot.h"

SmallDot::SmallDot(){

	mpRect = C_Rectangle{ 0,0,TILE_SIZE / 4 ,TILE_SIZE / 4 };


}

SmallDot::~SmallDot(){

}

void SmallDot::init(int x, int y){
	mpRect.x = x + 3 * TILE_SIZE / 8;
	mpRect.y = y + 3 * TILE_SIZE / 8;

}

void SmallDot::render(){
	if (Entity::mpAlive) {
		ofSetColor(255, 255, 255);
		Entity::render();
	}
}
