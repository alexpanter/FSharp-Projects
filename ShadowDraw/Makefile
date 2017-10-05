EXTERNALS=window.fsx

libs: $(EXTERNALS)
	fsharpc -a $(EXTERNALS)

build: $(EXTERNALS)
	fsharpc main.fsx

run:
	mono main.exe

