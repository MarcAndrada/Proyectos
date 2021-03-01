#include "ofApp.h"

const clock_t begin_time = clock();
clock_t old_time;
clock_t new_time;

unsigned int global_delta_time = 0;

bool key_pressed[255];
bool key_down[255];
bool key_released[255];

//--------------------------------------------------------------
void ofApp::setup(){
	for (int i = 0; i < 255; i++)
	{
		key_pressed[i] = false;
		key_down[i] = false;
		key_released[i] = false;
	}
	//Time
	ofSetFrameRate(60);
	old_time = begin_time;
	new_time = begin_time;
	gameScene = new Scene;
	gameScene->init();
}

//--------------------------------------------------------------
void ofApp::update(){
	//delta time update (Se tiene que hacer Siempre)
	old_time = new_time;
	new_time = clock() - begin_time;
	global_delta_time = int(new_time - old_time);
	//-------------------------------------------------
	
	//Aqui va el juego
	gameScene->update();

	//-------------------------------------------------
	//revisamos la tecla que estamos apretando
	for (size_t i = 0; i < 255; i++)
	{
		key_pressed[i] = false;
		key_released[i] = false;
	}
	
}

//--------------------------------------------------------------
void ofApp::draw(){
	ofClear(0, 0, 0);
	gameScene->render();
}

//--------------------------------------------------------------
void ofApp::keyPressed(int key){
	if (key >= 255 || key < 0) {
		return;
	}
	if (!key_down[key]) { //Si no estoy manteniendo la tecla
		key_pressed[key] = true;//Es que la acabo de pulsar
	}
	key_down[key] = true;
	key_released[key] = false;
}

//--------------------------------------------------------------
void ofApp::keyReleased(int key){
	if (key >= 255 || key < 0) {
		return;
	}
	
	key_pressed[key] = false;
	key_down[key] = false;
	key_released[key] = true;
}

//--------------------------------------------------------------
void ofApp::mouseMoved(int x, int y ){

}

//--------------------------------------------------------------
void ofApp::mouseDragged(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mousePressed(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mouseReleased(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mouseEntered(int x, int y){

}

//--------------------------------------------------------------
void ofApp::mouseExited(int x, int y){

}

//--------------------------------------------------------------
void ofApp::windowResized(int w, int h){

}

//--------------------------------------------------------------
void ofApp::gotMessage(ofMessage msg){

}

//--------------------------------------------------------------
void ofApp::dragEvent(ofDragInfo dragInfo){ 

}
