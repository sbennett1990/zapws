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

# Which C# language version to compile with
LANG=	7.3

FLAGS=	-nologo -main:${MAIN} -out:${BINDIR}/${PROG}
FLAGS+=	-langversion:\"${LANG}\"
.if defined(APPCONFIG)
FLAGS+=	-appconfig:${APPCONFIG}
.endif
DFLAGS=	-debug -optimize-

# Real location of the C# source files
CS=	${SRCS:S,^,$(SRCDIR)/,g}
all: testobj
	csc ${FLAGS} ${CS} ${PROPS}

debug: testobj
	csc ${FLAGS} ${DFLAGS} ${CS} ${PROPS}

doc: testobj
.if defined(DOCFILE)
	csc ${FLAGS} -doc:${DOCFILE} ${CS}
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
	mkdir ${BINDIR}
.endif

clean:
	rm -f ${PROG} ${DOCFILE} *.pdb
