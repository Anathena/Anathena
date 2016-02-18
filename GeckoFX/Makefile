#!/usr/bin/make -f

FF_MAJOR=`firefox --version|cut -d\  -f3|cut -d\. -f1`
FF_VERSION=`firefox --version|cut -d\  -f3`
VERSION="$(FF_VERSION).1"
CONFIGURATION ?= Debug_Linux

all: Geckofx

clean:
	xbuild /p:Configuration=$(CONFIGURATION) Geckofx.sln /target:clean
	rm -rf */obj */bin
	cd Geckofx-Core/Linux && make clean

Geckofx: Geckofx.sln
	echo "Configuration=$(CONFIGURATION); VERSION=$(VERSION)"
	xbuild /p:Configuration=$(CONFIGURATION) Geckofx.sln
	cd Geckofx-Core/Linux && make && cp geckofix.so ../bin/x86/$(CONFIGURATION)/

test: Geckofx
	cp GeckoFxTest/GeckoFxTest.sh GeckoFxTest/bin/x86/$(CONFIGURATION)/
	cp Geckofx-Core/Linux/geckofix.so GeckoFxTest/bin/x86/$(CONFIGURATION)
	cd GeckoFxTest/bin/x86/$(CONFIGURATION) &&  LD_LIBRARY_PATH=${LD_LIBRARY_PATH}:/usr/lib/firefox/ MONO_PATH=/usr/lib/cli/gdk-sharp-2.0/ LD_PRELOAD=../../../Geckofx-Core/Linux/geckofix.so PATH=/usr/local/bin:${PATH} ./GeckoFxTest.sh

unittest: GeckofxUnitTests/GeckofxUnitTests.csproj
	cd GeckofxUnitTests && xbuild GeckofxUnitTests.csproj

runtests: unittest Geckofx
	cd GeckofxUnitTests/bin/Debug && LD_LIBRARY_PATH=${LD_LIBRARY_PATH}:/usr/lib/firefox/ MONO_PATH=/usr/lib/cli/gdk-sharp-2.0/ LD_PRELOAD=../../../Geckofx-Core/Linux/geckofix.so PATH=/usr/local/bin:${PATH} mono GeckofxUnitTests.exe

tarclean: clean
	-rm *.tar.gz
	-rm ../geckofx*.tar.gz

dist: tarclean
	tar --exclude-vcs --exclude-backups --exclude=obj --exclude=bin --exclude=debian --exclude=PutXulRunnerFolderHere --exclude=".*~" -czf ../geckofx-$(VERSION).tar.gz .
	mkdir geckofx-$(VERSION) && cd geckofx-$(VERSION) && tar xfz ../../geckofx-$(VERSION).tar.gz
	tar czf geckofx-$(VERSION).tar.gz geckofx-$(VERSION)
	rm -rf ../geckofx-$(VERSION).tar.gz geckofx-$(VERSION)

debiandist: dist
	cd .. && ln -s geckofx-${FF_MAJOR}.0/geckofx-$(VERSION).tar.gz geckofx_$(VERSION).orig.tar.gz
	cd .. && rm -rf geckofx-$(VERSION) && tar xfz geckofx_$(VERSION).orig.tar.gz
	cd ../geckofx-$(VERSION) && cp -a ../geckofx-deb/debian .

testpackagebuild: debiandist
	cd ../geckofx-$(VERSION) && debuild -us -uc

install: Geckofx
	install -d $(DESTDIR)
	install GeckoFxTest/bin/x86/$(CONFIGURATION)/Geckofx-Core.dll $(DESTDIR)/Geckofx-Core.dll
	install GeckoFxTest/bin/x86/$(CONFIGURATION)/Geckofx-Core.dll.config $(DESTDIR)/Geckofx-Core.dll.config
	install GeckoFxTest/bin/x86/$(CONFIGURATION)/Geckofx-Winforms.dll $(DESTDIR)/Geckofx-Winforms.dll
	install Geckofx-Core/Linux/geckofix.so $(DESTDIR)/geckofix.so
	install -d $(DESTDIR)/pkgconfig
	install Geckofx-Core/Geckofx-Core.pc $(DESTDIR)/pkgconfig/Geckofx-Core.pc
	install Geckofx-Winforms/Geckofx-Winforms.pc $(DESTDIR)/pkgconfig/Geckofx-Winforms.pc
	chmod -x $(DESTDIR)/pkgconfig/*.pc
