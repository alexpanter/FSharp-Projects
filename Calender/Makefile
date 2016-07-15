# This is the Makefile for the Calender application.

MAIN=main.fsx
FILES=classes.fs control.fsx messages.fs
LIB=libcal.dll
OUT=calender

libs: ${FILES} ${LIB}
	fsharpc -o ${LIB} -a ${FILES}

build: ${LIB} ${OUT}
	fsharpc ${LIB} -o ${OUT}

test: ${MAIN}
	fsharpc -o $@.exe ${MAIN}
	mono test.exe
