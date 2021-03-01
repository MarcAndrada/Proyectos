#include "Scene.h"

Scene::Scene(){

}

Scene::~Scene(){

}

void Scene::init(){
	
	mpMapW = 0;
	mpMapW = 0;
	mpPlayer = new Pacman();
	initMap();
	font1.loadFont("PressStart2P.ttf", 20);
	SetMaxScore();
	totalDots = DotNum;
}

void Scene::update(){
	if (win) {
		ofSleepMillis(3000);
		win = false;
	}
	if (DotNum <= 110) {
		DotNum = 0;
		win = true;
	}
	std::cout << DotNum << std::endl;
	if (end) {
		ofSleepMillis(5000);
		if (lose){
			EndOfGame();
		}
		
	}
	mpPlayer->update();

	C_Rectangle rectPlayer = mpPlayer->getRect();

	int Size = mEntities.size();
	for (size_t i = 0; i < Size; i++)
	{
		if (mEntities[i]->isAlive()) {
			mEntities[i]->update();
			C_Rectangle rectEntity = mEntities[i]->getRect();
			int collision = mpPlayer->collidesWithEntity(mEntities[i]);
			switch (collision)
			{
			default:
				break;

			case SMALLDOT:
				mEntities[i]->setAlive(false);
				scoreCount += 10;
				DotNum--;
				break;
			case POWERUP:
				mEntities[i]->setAlive(false);
				mpPlayer->changeState();
				scoreCount += 100;
				for (int i = totalDots; i < Size; i++)
				{
					mEntities[i]->changeState();
				}
				DotNum--;
				break;
			case GHOST: 
				if (mpPlayer->getState()){
					mEntities[i]->setAlive(false);
					scoreCount += 500;
				}else if (!mpPlayer->getState()){
					mpPlayer->setAlive(false);
					dead = true;
				}
				
				break;
			}
			if (scoreCount > MaxScore) {
				MaxScore = scoreCount;
			}
		}
	}
	
/*
	int size = mDots.size();
	for (size_t i = 0; i < size; i++)
	{
		if (mDots[i]->isAlive()) {
			C_Rectangle rectDot = mDots[i]->getRect();
			if (C_RectangleCollision(rectPlayer, rectDot))
			{
				mDots[i]->setAlive(false);
			}
		}
	}

	int PUsize = mPowerUp.size();

	for (size_t i = 0; i < PUsize; i++)
	{
		if (mPowerUp[i]->isAlive()) {
			C_Rectangle rectPU = mPowerUp[i]->getRect();
			if (C_RectangleCollision(rectPlayer, rectPU))
			{
				mpPlayer->changeState();
				for (size_t i = 0; i < GhostSize; i++)
				{
					mGhost[i]->changeState();
				}
				mPowerUp[i]->setAlive(false);
			}
		}
	}
	*/
	
}

void Scene::render(){
	ofSetColor(255, 255, 255);
	font1.drawString(score, 650, 60);
	font1.drawString(std::to_string(scoreCount), 650, 100);
	font1.drawString(currentMaxScoreString, 650, 200);
	font1.drawString(std::to_string(MaxScore), 650, 240);
	font1.drawString(livesLeft, 650, 400);
	font1.drawString(std::to_string(lives), 650, 440);
	renderMap();
	int size = mEntities.size();
	for (size_t i = 0; i < size; i++)
	{
		if (mEntities[i]->isAlive()){
			mEntities[i]->render();
		}
	}

	mpPlayer->render();

	ofSetColor(255, 255, 255);
	if (win) {
		endGame = "YOU WIN";
		font1.drawString(endGame, 215, 290);
		restartGame();	

	}else if (lose){
		endGame = "YOU LOSE";
		font1.drawString(endGame, 215, 290);
	}
	else if (dead) {
		restartGame();
	}
}

void Scene::initMap(){
	std::fstream Map;
	Map.open("map.txt", std::ios::in);
	if (!Map.is_open()) {
		std::cout << "Error" << std::endl;
		system("pause");
		exit(0);
	}
	std::string line;
	std::getline(Map, line);
	int width = atoi(line.c_str());
	std::getline(Map, line);
	int height = atoi(line.c_str());

	std::vector<Entity*> background;
	std::vector<Entity*> foreground;
		
	mpCollisionMap.resize(height);
	for (size_t i = 0; i < height; i++)
	{
		mpCollisionMap[i].resize(width, false);
		std::getline(Map, line);
		for (size_t j = 0; j < width; j++)
		{
			char a_char = line[j];
			switch (a_char) {
			case '#':
				mpCollisionMap[i][j] = true;
				break;
			case 'P':
				//crear Pacman
				mpPlayer->init(j*TILE_SIZE,i*TILE_SIZE);
				mpPlayer->setCollisionsMap(&mpCollisionMap, width, height);
				PacmanPosI = i;
				PacmanPosJ = j;
				break;
			case '.': 
				{
				//crear punto
				SmallDot* aDot = new SmallDot;
				aDot->init(j * TILE_SIZE, i * TILE_SIZE);
				background.push_back(aDot);
				DotNum++;
				}break;

			case 'O':
				{
				//crear powerup
				PowerUp* PUp = new PowerUp;
				PUp->init(j * TILE_SIZE, i * TILE_SIZE);
				background.push_back(PUp);
				DotNum++;
				}break;
			case 'F':
				//crear Fantasma
				Ghost* aGhost = new Ghost;
				aGhost->init(j * TILE_SIZE, i * TILE_SIZE);
				aGhost->setCollisionsMap(&mpCollisionMap, width, height);
				foreground.push_back(aGhost);
				break;
			}
		}
	}
	Map.close();
	
	int BGsize = background.size();
	for (int i = 0; i < BGsize; i++)
	{
		mEntities.push_back(background[i]);
	}

	int FGsize = foreground.size();
	for (int i = 0; i < FGsize; i++)
	{
		mEntities.push_back(foreground[i]);
	}
	mpMapW = width;
	mpMapH = height;
}

void Scene::renderMap(){

	ofSetColor(0, 0, 255);
	for (int i = 0; i < mpMapH; i++) {
		for (int j = 0; j < mpMapW; j++){
			
			if (mpCollisionMap[i][j]) {
				ofDrawRectangle(j * TILE_SIZE, i * TILE_SIZE, TILE_SIZE, TILE_SIZE);

			}
		}

	}
}

void Scene::EndOfGame(){
	if (scoreCount == MaxScore) {
		std::fstream MaxScore_file;
		MaxScore_file.open("max_score.txt", std::ios::out | std::ios::trunc);
		MaxScore_file << scoreCount;
		MaxScore_file.close();
	}
	exit(-1);

}

void Scene::SetMaxScore(){
	MaxScore = 0;
	std::fstream MaxScore_file;
	MaxScore_file.open("max_score.txt", std::ios::in);
	std::getline(MaxScore_file, ScoreString);
	MaxScore = atoi(ScoreString.c_str());
	MaxScore_file.close();

}

void Scene::restartGame()
{
	
	if (win)
	{
		mEntities.clear();
		initMap();
		int Size = mEntities.size();
		for (int i = DotNum; i < Size; i++)
		{
			mEntities[i]->speedUp();
		}
	}

	if (dead) {
		if (lives > 1) {
			delete mpPlayer;
			mpPlayer = new Pacman();
			mpPlayer->init(PacmanPosJ * TILE_SIZE, PacmanPosI * TILE_SIZE);
			mpPlayer->setCollisionsMap(&mpCollisionMap, mpMapW, mpMapH);
			lives--;
		}else{
			end = true;
			lose = true;
			render();
			return;
		}
	}
	
	dead = false;
	end = false;
	lose = false;
	

}


