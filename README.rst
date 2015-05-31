Hawkmatix Distribution
======================

Hawkmatix creates upper and lower bounds for the variance in price based on
probability distributions.

Installation
------------

Install from source::

    

Package Contents
----------------

    distribution/
        Distribution sources.

Usage
-----

This software is intended for use with the NinjaTrader trading platform.
Full documentation is available at http://Hawkmatix.github.io/distribution.html

Supported Operating Environment
-------------------------------

This version of the add-on software has been tested, and is known to work
against the following NinjaTrader versions and operating systems.

NinjaTrader Versions
~~~~~~~~~~~~~~~~~~~~

* NinjaTrader 7.0.1000.27
* NinjaTrader 6.5.1000.19

Operating Systems
~~~~~~~~~~~~~~~~~

* Windows 7/8

Requirements
------------

Supports NinjaTrader 6.5.1000.19 - 7.0.1000.27. Uses ``HawkmatixAvgDiff``
(packaged here).

License
-------

All code contained in this repository is Copyright 2012-Present Andrew C.
Hawkins.

This code is released under the GNU Lesser General Public License. Please see
the COPYING and COPYING.LESSER files for more details.

Contributors
------------

* Andrew C. Hawkins <andrew@hawkmatix.com>

Changelog
---------

* v3 Added Hawkmatix Average Difference so the Laplace Distribution can be
determined.

* v2 Additional distributions are added (Laplace and Logistic).

* v1 Standard calculations to solve for the upper and lower bounds using the
Gaussian Distribution, given a probability, are implemented.
