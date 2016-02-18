""" GDB Python customization auto-loader for js shell """

import os.path
sys.path[0:0] = [os.path.join('c:/builds/moz2_slave/rel-m-rel-xr_w32_bld-000000000/build/js/src', 'gdb')]

import mozilla.autoload
mozilla.autoload.register(gdb.current_objfile())
