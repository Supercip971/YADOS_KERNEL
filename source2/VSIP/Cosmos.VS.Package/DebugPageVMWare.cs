﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cosmos.Build.Common;

namespace Cosmos.VS.Package
{
    public partial class DebugPageVMWare : SubPropertyPageBase
    {
        public DebugPageVMWare()
        {
            InitializeComponent();

            comboDebugMode.Items.AddRange(EnumValue.GetEnumValues(typeof(DebugMode), false));
            comboDebugMode.SelectedIndexChanged += delegate(Object sender, EventArgs e)
            {
                var x = (DebugMode)((EnumValue)comboDebugMode.SelectedItem).Value;
                if (x != mProps.DebugMode)
                {
                    mProps.DebugMode = x;
                    IsDirty = true;
                }
            };

            comboTraceMode.Items.AddRange(EnumValue.GetEnumValues(typeof(TraceAssemblies), false));
            comboTraceMode.SelectedIndexChanged += delegate(Object sender, EventArgs e)
            {
                var x = (TraceAssemblies)((EnumValue)comboTraceMode.SelectedItem).Value;
                if (x != mProps.TraceAssemblies)
                {
                    mProps.TraceAssemblies = x;
                    IsDirty = true;
                }
            };

            checkEnableGDB.CheckedChanged += delegate(Object sender, EventArgs e)
            {
                bool x = checkEnableGDB.Checked;
                if (x != mProps.EnableGDB)
                {
                    mProps.EnableGDB = x;
                    IsDirty = true;
                }
                checkStartCosmosGDB.Enabled = x;

                // Only change it if its in response to user click, not loading
                if (mPropsFilled) {
                    checkStartCosmosGDB.Checked = checkEnableGDB.Checked;
                }
            };

            checkStartCosmosGDB.CheckedChanged += delegate(Object sender, EventArgs e)
            {
                bool x = checkStartCosmosGDB.Checked;
                if (x != mProps.StartCosmosGDB)
                {
                    mProps.StartCosmosGDB = x;
                    IsDirty = true;
                }
            };
        }

        protected DebugProperties mProps = new DebugProperties();
        public override PropertiesBase Properties
        {
            get { return mProps; }
        }

        protected bool mPropsFilled = false;
        public override void FillProperties()
        {
            base.FillProperties();
            mProps.Reset();

            mProps.SetProperty("EnableGDB", GetConfigProperty("EnableGDB"));
            checkEnableGDB.Checked = mProps.EnableGDB;

            mProps.SetProperty("StartCosmosGDB", GetConfigProperty("StartCosmosGDB"));
            checkStartCosmosGDB.Checked = mProps.StartCosmosGDB;

            mProps.SetProperty("DebugMode", GetConfigProperty("DebugMode"));
            comboDebugMode.SelectedItem = EnumValue.Find(comboDebugMode.Items, mProps.DebugMode);

            mProps.SetProperty("TraceMode", GetConfigProperty("TraceMode"));
            comboTraceMode.SelectedItem = EnumValue.Find(comboTraceMode.Items, mProps.TraceAssemblies);

            mPropsFilled = true;
        }

    }
}
