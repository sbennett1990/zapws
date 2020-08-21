# Build zapws for Mono on OpenBSD

PROG=	zapws.exe

SRCS=	zapws.cs

MAIN=	Program
APPCONFIG=
DOCFILE=	zapws.doc

FLAGS=	-nologo -main:${MAIN} -out:${PROG}
FLAGS+=	-langversion:\"7.3\"
#FLAGS+=	-appconfig:${APPCONFIG}
DFLAGS=	-debug -optimize-

all:
	csc ${FLAGS} ${SRCS}

debug:
	csc ${FLAGS} ${DFLAGS} ${SRCS}

doc:
	csc -doc:${DOCFILE} ${SRCS}

clean:
	rm -f ${PROG} ${DOCFILE} *.pdb
