all: geckofix.so

geckofix.so: mozilla-release/obj-xulrunner/mfbt/Makefile
	make -C mozilla-release/obj-xulrunner/mfbt
	make -C mozilla-release/obj-xulrunner/mozglue/build
	g++ --shared -lstdc++ -o geckofix.so\
 mozilla-release/obj-xulrunner/mfbt/*.o mozilla-release/obj-xulrunner/mozglue/build/*.o

clean:
	rm -rf geckofix.so mozilla-release xulrunner-29.0.1.source.tar.bz2

mozilla-release/obj-xulrunner/mfbt/Makefile: mozilla-release/.mozconfig
	make -C mozilla-release -f client.mk configure export

mozilla-release/.mozconfig: xulrunner-29.0.1.source.tar.bz2
	tar xf xulrunner-29.0.1.source.tar.bz2
	echo 'mk_add_options MOZ_OBJDIR=@TOPSRCDIR@/obj-xulrunner'> mozilla-release/.mozconfig
	echo 'mk_add_options MOZ_MAKE_FLAGS="-j4"'>> mozilla-release/.mozconfig
	echo 'ac_add_options --enable-application=xulrunner'>> mozilla-release/.mozconfig
	echo 'ac_add_options --disable-crashreporter --disable-tests'>> mozilla-release/.mozconfig

xulrunner-29.0.1.source.tar.bz2:
	wget -N http://ftp.mozilla.org/pub/mozilla.org/xulrunner/releases/29.0.1/source/xulrunner-29.0.1.source.tar.bz2
