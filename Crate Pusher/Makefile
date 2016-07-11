# This is the makefile for the game 'Crate Pusher'.

MAIN=main.fsx
FILES=maps/blueprint.fs maps/maps.fsx movement.fs
OUT=Crate\ Pusher.exe


test: ${MAIN}
	fsharpc -o $@.exe ${MAIN} 
	mono test.exe

release:
	fsharpc --standalone --target:winexe --out:$(OUT) $(FILES) $(MAIN)
