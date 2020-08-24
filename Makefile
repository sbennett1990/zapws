# Build zapws for Mono on OpenBSD

PROG=	zapws.exe

SRCS=	zapws.cs
PROPS=	Properties/AssemblyInfo.cs

MAIN=	Program
APPCONFIG=	App.config
DOCFILE=	zapws.doc

SRCDIR= src
BINDIR=	bin
.poison empty (BINDIR)

# Enumerate libraries used
LIBS=	libcmdline

# Which C# language version to compile with
LANG=	7.3

# Set compiler flags
.ifmake debug
OUT=	${BINDIR}/Debug/${PROG}
.else
OUT=	${BINDIR}/${PROG}
.endif

FLAGS=	-nologo -main:${MAIN} -out:${OUT}
FLAGS+=	-langversion:\"${LANG}\"
.if defined(APPCONFIG)
FLAGS+=	-appconfig:${APPCONFIG}
.endif
# Add in libraries
.if defined(LIBS)
.  ifmake debug
FLAGS+=	-lib:${BINDIR}/Debug
.  else
FLAGS+=	-lib:${BINDIR}
.  endif
.  for l in ${LIBS}
FLAGS+=	-reference:${l}.dll
.  endfor
.endif
DFLAGS=	-debug -define:DEBUG -optimize-

# Real location of the C# source files
CS=	${SRCS:S,^,$(SRCDIR)/,g}

all release: testobj libs
	csc ${FLAGS} ${CS} ${PROPS}
	@echo '${PROG:R} -> ${.CURDIR}/${OUT}'

debug: testobj libs
	csc ${FLAGS} ${DFLAGS} ${CS} ${PROPS}
	@echo '${PROG:R} -> ${.CURDIR}/${OUT}'

doc: testobj
.if defined(DOCFILE)
	csc ${FLAGS} -doc:${DOCFILE} ${CS}
	@echo '${PROG:R} -> ${.CURDIR}/${OUT}'
.else
	@echo 'No DOCFILE defined, nothing to do'
.endif

testobj:
.if !exists(${BINDIR})
	@echo 'No output directory! Run `make obj` before proceeding'
	@false
.endif

obj:
.if !exists(${BINDIR})
	mkdir -p ${BINDIR}/Debug
.endif

libs:
.if defined(LIBS)
	@echo '===> building libs'
.  ifmake debug
	cd ${.CURDIR}/lib && make obj && make debug
.    for l in ${LIBS}
	@cp ${.CURDIR}/lib/bin/Debug/${l}{.dll,.pdb} ${BINDIR}/Debug
.    endfor
.  else
	cd ${.CURDIR}/lib && make obj && exec make
.    for l in ${LIBS}
	@cp ${.CURDIR}/lib/bin/${l}.dll ${BINDIR}
.    endfor
.  endif
	@echo '===> done with libs'
.endif

clean:
	rm -rf ${BINDIR}
	rm -f ${PROG} ${DOCFILE}
	@mkdir -p ${BINDIR}/Debug
