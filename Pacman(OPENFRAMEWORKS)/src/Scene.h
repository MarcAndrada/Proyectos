#ifndef SCENE_H
#define SCENE_H

#include "ofMain.h"
#include "Utils.h"
#include "Pacman.h"
#include "SmallDot.h"
#include "PowerUp.h"
#include "Ghost.h"
#include "Entity.h"
class Scene {
	public:
		Scene();
		~Scene();

		void init();
		void update();
		void render();

	private:

		void initMap();
		void renderMap();
		int totalDots;
		int DotNum = 0;
		std::vector<std::vector<bool>> mpCollisionMap;
		int mpMapW;
		int mpMapH;
		int scoreCount = 0;
		std::string score = "Score";
		std::string currentMaxScoreString = "Max Score";
		void EndOfGame();
		void SetMaxScore();
		int MaxScore;
		std::string ScoreString;
		std::string endGame;
		bool win = false;
		bool dead = false;
		bool lose = false;
		bool end = false;
		void restartGame();
		int lives = 3;
		std::string livesLeft = "Lives Left";
		int PacmanPosI;
		int PacmanPosJ;

		Pacman* mpPlayer;
		std::vector<Entity*>mEntities;
		ofTrueTypeFont font1;
};

#endif 

