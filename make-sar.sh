#!/usr/bin/env bash

APP_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
YYYYMMDD=`date '+%Y%m%d'`
INSTALLER_NAME="InstallJa${YYYYMMDD}.sar"

echo ${INSTALLER_NAME}

cd $APP_DIR/installerBuild
cp -r ../patches .
zip ${INSTALLER_NAME} -r .
mv ${INSTALLER_NAME} ../installers/
rm -r ./patches
