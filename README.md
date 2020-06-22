# TinaX Framework - UIKit extension for ILRuntime.

<img src="https://github.com/yomunsam/TinaX.Core/raw/master/readme_res/logo.png" width = "360" height = "160" alt="logo" align=center />

[![LICENSE](https://img.shields.io/badge/license-NPL%20(The%20996%20Prohibited%20License)-blue.svg)](https://github.com/996icu/996.ICU/blob/master/LICENSE)
<a href="https://996.icu"><img src="https://img.shields.io/badge/link-996.icu-red.svg" alt="996.icu"></a>
[![LICENSE](https://camo.githubusercontent.com/890acbdcb87868b382af9a4b1fac507b9659d9bf/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f6c6963656e73652d4d49542d626c75652e737667)](https://github.com/yomunsam/TinaX/blob/master/LICENSE)

<!-- [![LICENSE](https://camo.githubusercontent.com/3867ce531c10be1c59fae9642d8feca417d39b58/68747470733a2f2f696d672e736869656c64732e696f2f6769746875622f6c6963656e73652f636f6f6b6965592f596561726e696e672e737667)](https://github.com/yomunsam/TinaX/blob/master/LICENSE) -->

TinaX is a Unity-based framework, simple , complete and delightful, ready to use.

TinaX provides functionality in the form of "Unity packages". 

`TinaX.UIKit.ILRuntime` is an extension package for [TinaX.ILRuntime](https://github.com/yomunsam/TinaX.ILRuntime), which is used for more pleasant use of UIKit in hot update code.

<br>

package name: `io.nekonya.tinax.uikit.ilruntime`

<br>

"Readme" in other languages :

- [简体中文](README_CN.md)

<br>

------

## Usage

Invoke register method when framework is initializing:

``` csharp
using TinaX;
using TinaX.XILRuntime;
using TinaX.XILRuntime.Registers;

namespace Nekonya.Example
{
    public class AppBootstrap : IXBootstrap
    {
        public void OnInit(IXCore core)
        {
            var xil = core.GetService<IXILRuntime>(); 
            xil.RegisterUIKit(); //extension method, namespace "TinaX.XILRuntime.Registers"
        }
        public void OnStart(IXCore core) { }
        public void OnQuit() { }
        public void OnAppRestart() { }
    }
}
```

<br>

------

## Install this package

### Install via [openupm](https://openupm.com/)

``` bash
# Install openupm-cli if not installed.
npm install -g openupm-cli
# OR yarn global add openupm-cli

#run install in your project root folder
openupm add io.nekonya.tinax.uikit.ilruntime
```

<br>

### Install via npm (UPM)

Modify `Packages/manifest.json` file in your project, and add the following code before "dependencies" node of this file:

``` json
"scopedRegistries": [
    {
        "name": "TinaX",
        "url": "https://registry.npmjs.org",
        "scopes": [
            "io.nekonya",
            "com.ourpalm"
        ]
    },
    {
        "name": "package.openupm.com",
        "url": "https://package.openupm.com",
        "scopes": [
            "com.cysharp.unitask",
            "com.neuecc.unirx"
        ]
    }
],
```

If after doing the above, you still cannot find the relevant Packages for TinaX in the "Unity Package Manager" window, You can also try refreshing, restarting the editor, or manually adding the following configuration to "dependencies" node.

``` json
"io.nekonya.tinax.uikit.ilruntime" : "6.6.1"
```

<br>

### Install via git UPM:

You can use the following to install and use this package in UPM GUI.  

```
git://github.com/yomunsam/TinaX.UIKit.ILRuntime.git
```

If you want to set a target version, you can use release tag like `#6.6.1`. for detail you can see this page: [https://github.com/yomunsam/TinaX.UIKit.ILRuntime/releases](https://github.com/yomunsam/TinaX.UIKit.ILRuntime/releases)



<br><br>
------

## Dependencies

- [io.nekonya.tinax.ilruntime](https://github.com/yomunsam/TinaX.ILRuntime) :`git://github.com/yomunsam/TinaX.ILRuntime.git`
- [io.nekonya.tinax.uikit](https://github.com/yomunsam/TinaX.UIKit) :`git://github.com/yomunsam/TinaX.UIKit.git`
- [io.nekonya.tinax.xcomponent.ilruntime](https://github.com/yomunsam/TinaX.XComponent.ILRuntime) :`git://github.com/yomunsam/TinaX.XComponent.ILRuntime.git`

> if you install packages by git UPM， You need to install the dependencies manually. Or dependencies will installed automatically by NPM / OpenUPM

<br><br>

------

## Learn TinaX

You can find out how to use the various features of TinaX in the [documentation](https://tinax.corala.space)

------

## Third-Party

The following excellent third-party libraries are used in this project:

- **[ILRuntime](https://github.com/Ourpalm/ILRuntime)** : Pure C# IL Intepreter Runtime, which is fast and reliable for scripting requirement on enviorments, where jitting isn't possible.
