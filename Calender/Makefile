# This is the Makefile for the Calender application.

MAIN=main.fsx
FILES=classes.fs control.fsx messages.fs
LIB=libcal.dll
OUT=calender.exe

libs:
	fsharpc -o ${LIB} -a ${FILES}

build: ${LIB}
	fsharpc -r ${LIB} -o ${OUT} ${MAIN}

