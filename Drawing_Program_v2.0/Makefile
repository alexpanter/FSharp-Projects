# This is a Windows Makefile.
# Run the commands in command prompt, e.g. 'nmake release'

# Convert to UNIX Makefile by changing all parantheses to curly braces:
# $(FILES) -> ${FILES}    and
# '\' -> '/'    (to UNIX path separator)

# Also, for properly linking the resource files, this project uses the
# Microsoft F# Compiler 'fsc.exe'.


DRAWING_FILES=lib\drawing_shapes.fs lib\drawing_math.fs 
DRAWING_DLL=lib\drawing.dll

GRAPHICS_FILES=lib\graphics_graphics.fs lib\graphics_dialogs.fs
GRAPHICS_DLL=lib\graphics.dll

INSTANCE_FILES=lib\instance_FileInstance.fsx lib\instance_MainInstance.fsx lib\instance_Controls.fsx lib\instance_Menu.fsx lib\instance_IO.fsx lib\instance_Settings.fsx
INSTANCE_DLL=lib\instance.dll

READ_LIBRARIES=-r $(DRAWING_DLL) -r $(GRAPHICS_DLL) -r $(INSTANCE_DLL) -r lib\globals.dll

RESOURCE=resources\releaseinfo.res
RELEASE=Shadow Draw.exe


globals:
	fsc --out:lib\globals.dll -a lib\settings_globals.fs

libs:
	fsc -r lib\globals.dll --out:$(DRAWING_DLL) -a $(DRAWING_FILES)
	fsc -r lib\globals.dll --out:$(GRAPHICS_DLL) -a $(GRAPHICS_FILES)
	fsc --out:$(INSTANCE_DLL) -a $(INSTANCE_FILES)

documentation:
	fsc --doc:lib\drawing_documentation.xml --out:$(DRAWING_DLL) -a $(DRAWING_FILES)
	fsc --doc:lib\graphics_documentation.xml --out:$(GRAPHICS_DLL) -a $(GRAPHICS_FILES)
	fsc --doc:lib\instance_documentation.xml --out:$(INSTANCE_DLL) -a $(INSTANCE_FILES)

test:
	fsc --standalone $(READ_LIBRARIES) --out:test.exe main.fsx

release:
	fsc --standalone --target:winexe --win32res:$(RESOURCE) --out:"$(RELEASE)" $(READ_LIBRARIES) main.fsx

