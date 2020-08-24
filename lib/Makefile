# Build libs for Mono on OpenBSD

LIB=	libcmdline

SRCS=	CommandLineProcessor.cs Option.cs OptionEventArgs.cs
SRCS+=	Properties/AssemblyInfo.cs

DOCFILE=	libcmdline.doc

SRCDIR= ${LIB}
BINDIR=	bin
.poison empty (BINDIR)

# Which C# language version to compile with
LANG=	7.3

# Set compiler flags
.ifmake debug
OUT=	${BINDIR}/Debug/${LIB}.dll
.else
OUT=	${BINDIR}/${LIB}.dll
.endif

FLAGS=	-nologo -target:library -out:${OUT}
FLAGS+=	-langversion:\"${LANG}\"
DFLAGS=	-debug -define:DEBUG -optimize-

# Real location of the C# source files
CS=	${SRCS:S,^,$(SRCDIR)/,g}

all release: testobj
	csc ${FLAGS} ${CS}
	@echo '${LIB} -> ${.CURDIR}/${OUT}'

debug: testobj
	csc ${FLAGS} ${DFLAGS} ${CS}
	@echo '${LIB} -> ${.CURDIR}/${OUT}'

doc: testobj
.if defined(DOCFILE)
	csc ${FLAGS} -doc:${DOCFILE} ${CS}
	@echo '${LIB} -> ${.CURDIR}/${OUT}'
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

clean:
	rm -rf ${BINDIR}
	rm -f ${LIB}.dll ${DOCFILE}
	@mkdir -p ${BINDIR}/Debug
