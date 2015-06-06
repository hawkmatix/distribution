Hawkmatix Distribution
======================

When assuming price follows a probability distribution, every price value has
an associated probability. This project uses continuous probability
distributions to determine low probability prices. Given a period and a
probability, the prices that satisfy the distribution are found and plotted.
The probability of price reaching each band is equivalent to the input
probability. The three distributions currently available are Gaussian, Laplace,
and Logistic. Additional distributions will be added soon.

Installation
------------

Install from source, method 1 (Requires Python):

    1. ``> python setup.py``
    2. Follow the directions after the script completes.

Install from source, method 2:

    1. Unzip the downloaded file ``distribution-master.zip``.
    2. Locate the source file ``HawkmatixDistribution.cs``.
    3. Move the source file to the NinjaTrader indicator folder ``Documents/
        NinjaTrader 7/bin/Custom/Indicator``.
    4. Open any indicator in NinjaTrader by going to Tools > Edit NinjaScript
        > Indicator...
    5. Press the ``compile`` button in the menu bar.

Package Contents
----------------

    distribution/
        Distribution sources.

Usage
-----

This software is intended for use with the NinjaTrader trading platform.
Full documentation is available at http://hawkmatix.github.io/distribution.html

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
