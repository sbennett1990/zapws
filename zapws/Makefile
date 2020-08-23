# Build zapws for Mono on OpenBSD

PROG=	zapws.exe

SRCS=	zapws.cs
PROPS=	Properties/AssemblyInfo.cs

MAIN=	Program
APPCONFIG=	App.config
DOCFILE=	zapws.doc

FLAGS=	-nologo -main:${MAIN} -out:${PROG}
FLAGS+=	-langversion:\"7.3\"
FLAGS+=	-appconfig:${APPCONFIG}
DFLAGS=	-debug -optimize-

# Real location of the C# source files
CS=	${SRCS:S,^,src/,g}
all:
	csc ${FLAGS} ${CS} ${PROPS}

debug:
	csc ${FLAGS} ${DFLAGS} ${CS} ${PROPS}

doc:
	csc -doc:${DOCFILE} ${SRCS}

clean:
	rm -f ${PROG} ${DOCFILE} *.pdb
