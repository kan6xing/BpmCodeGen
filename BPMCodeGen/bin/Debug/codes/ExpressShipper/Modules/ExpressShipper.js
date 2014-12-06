Ext.namespace("YZSoft.ExpressShipper");

YZSoft.ExpressShipper.OpenApplication = function(gridid,ID) {
    YZSoft.BPM.FormManager.OpenFormApplication('Express\寄件人信息', ID, 'Read', true, {
        title: '查看',
        width:830,
        height:505
    });
};

YZSoft.ExpressShipper.MoldOption = {
    DeleteStamp: function(grid) {
        sm = grid.getSelectionModel();
        recs = sm.getSelections() || new Array();

        if (recs.length == 0)
            return;
        
        var params = {};
        params.Method = 'delete';
        params.Count = recs.length;
        for (var i = 0; i < recs.length; i++) {
            params["ID" + i] = recs[i].data.TaskID;
        };

        Ext.Msg.show({
            title: '删除确认',
            msg: '您确定要删除选中的新闻吗？',
            buttons: Ext.Msg.OKCANCEL,
            icon: Ext.MessageBox.INFO,

            fn: function(btn, text) {
                if (btn != 'ok')
                    return;
                YZSoft.Ajax.RFC({
                    url: 'YZModules/Express/RFC/Delete_ExpressShipper.ashx',
                    params: params,
                    waitMsg: '正在删除...',
                    waitMsgTarget: grid.id,

                    success: function(action) {
                        var store = grid.getStore();
                        store.reload({ params: { start: store.cursor} });
                    },

                    failure: function(action) {
                        Ext.Msg.show({
                            title: '错误提示',
                            msg: YZSoft.HttpUtility.HtmlEncode(action.result.errorMessage, true),
                            buttons: Ext.Msg.OK,
                            icon: Ext.MessageBox.WARNING,
                            fn: function(btn, text) {
                                var store = grid.getStore();
                                store.reload({ params: { start: store.cursor} });
                            }
                        });
                    }
                });
            }
        });
    }
}

YZSoft.ExpressShipper.NewsManagePanel = Ext.extend(Ext.Panel, {
    constructor: function(config) {
        this.idKeyword = Ext.id(); 

        this.store = new YZSoft.Data.JsonStore({
            root: 'children',
            totalProperty: 'totalRows',
            idProperty: 'id',
            remoteSort: true,
            baseParams: { limit: YZSoft.EnvSetting.PageSize.defaultSize },
            fields: [
                { name: 'RowNumber' },
                { name: 'TaskID' },
                { name: 'Type' },
                { name: 'shipperStr' },{ name: 'shipperTelStr' },{ name: 'shipperPhoneStr' },{ name: 'shipperAdd7Str' }
            ],

            proxy: new Ext.data.HttpProxy({
                method: 'GET',
                url: 'YZModules/Express/StoreDataService/ExpressShipper.ashx'
            }),
            listeners: {
                scope: this,
                beforeload: function() {
                    var searchType = this.store.baseParams['SearchType'];
                    if (searchType == 'QuickSearch' || Ext.isEmpty(searchType)) {
                        this.store.baseParams['Keyword']=Ext.getCmp(this.idKeyword).getValue();
                    }
                },
                load: function() {
                    if (!Ext.isEmpty(this.grid.loadSelectId)) {
                        var sm = this.grid.getSelectionModel();
                        var rd = this.store.getById(this.grid.loadSelectId);
                        if (rd)
                            sm.selectRecords([rd]);
                        this.grid.loadSelectId = '';
                    }
                }
            }
        });

        this.store.setDefaultSort('TaskID', 'desc');
        this.grid = new Ext.grid.GridPanel({
            border: false,
            store: this.store,
            trackMouseOver: true,
            disableSelection: false,
            loadMask: true,
            region: 'center',
            stripeRows: true,

            // grid columns
            columns: [
                { header: '序号', dataIndex: 'RowNumber', width: 50, align: 'left' },
                { header: '编号', dataIndex: 'TaskID', width: 50, align: 'left', sortable: true, hidden: true },
                { header: '寄件人', dataIndex: 'shipperStr', width: 150, align: 'left' },{ header: '联系电话', dataIndex: 'shipperTelStr', width: 150, align: 'left' },{ header: '固定电话', dataIndex: 'shipperPhoneStr', width: 150, align: 'left' },{ header: '地址', dataIndex: 'shipperAdd7Str', id: 'extcol', align: 'left' }
            ],

            autoExpandColumn: 'extcol',
            bbar: new Ext.PagingToolbar({
                pageSize: this.store.baseParams.limit,
                store: this.store,
                displayInfo: true
            }),
            listeners: {
                scope: this,
                rowdblclick: function(grid, rowIndex, e) {
                    YZSoft.BPM.FormManager.OpenFormApplication('Express\寄件人信息', this.store.getAt(rowIndex).data.TaskID, 'Read', true, {
                        title: '查看',
                        width:830,
                        height:505
                    });
                }
            }
        });

        this.btnNew = new Ext.Button({
            iconCls: 'blist',
            text: '添加',
            scope: this,
            handler: function () {
                YZSoft.BPM.FormManager.OpenPostWindow('寄件人信息', true, {
                    title: '添加',
                    width: 830,
                    height: 505,
                    listeners: {
                        scope: this,
                        close: function (form) {
                            if (form.dialogResult == 'ok') {
                                this.store.reload({ params: { start: this.store.cursor } });
                            }
                        }
                    }
                });
                //    YZSoft.BPM.FormManager.OpenFormApplication('Express\寄件人信息', '', 'New', true, {
            //    title: '添加',
            //    width:830,
            //    height:505,
            //    listeners: {
            //        scope: this,
            //        close: function(form) {
            //            if (form.dialogResult=='ok') {
            //                this.grid.loadSelectId = form.returnValue.key;
            //                this.store.reload({ params: { start: this.store.cursor} });
            //            }
            //        }
            //    }
            //});
            }
        });

        this.btnEdit = new Ext.Button({
            iconCls: 'blist',
            text: '编辑',
            scope: this,
            handler: function() {
                var sm = this.grid.getSelectionModel();
                var recs = sm.getSelections() || new Array();

                if (recs.length != 1)
                    return;
                
                this.EditRow(recs[0].data);
            }
        });
        
        this.btnDelete = new Ext.Button({
            iconCls: 'blist',
            text: '删除',
            scope: this,
            handler: function() {
                YZSoft.ExpressShipper.MoldOption.DeleteStamp(this.grid);
            }
        });
        
        var cfg = {
            title: "草稿夹",
            closable: true,
            border: false,
            rootVisible: false,
            autoScroll: true,
            layout: 'border',
            items: [this.grid],
            tbar: [this.btnNew, this.btnEdit, this.btnDelete, '->',{
                xtype: 'button',
                scope: this,
                text: '清除搜索条件',
                handler: function() {
                    
                    this.store.baseParams = this.store.baseParams || {};
                    this.store.baseParams['SearchType'] = '';
                    this.store.reload({ params: { start: 0} });
                }
            },{
                xtype: 'YZSearchField',
                id: this.idKeyword,
                store: this.store,
                emptySearch: true,
                width: 160,
                showAdvButton: true,
                scope: this,
                handler: function(advBtn) {
                                        this.dlg = YZSoft.DialogManager.ShowDialogExt(this.idKeyword + '-dlgs',
                                          'YZModules/OA/Dialogs/OA_NewsDlg.js',
                                           {disableTaskState:true},
                                           {owner:advBtn,store:this.store});
                }
               }]
            };

            Ext.apply(cfg, config);

            YZSoft.ExpressShipper.NewsManagePanel.superclass.constructor.call(this, cfg);

            var sm = this.grid.getSelectionModel();
            if (sm) {
                sm.on('selectionchange', function() {
                    this.updateStatus();
                }, this);
            }
        },

        initComponent: function() {
            YZSoft.ExpressShipper.NewsManagePanel.superclass.initComponent.call(this);
            this.store.load({ params: { start: 0} });   
            this.updateStatus();
        },
        
        RenderName:function(value, p, record){
            return String.format("<a href='#' onclick=\"YZSoft.ExpressShipper.OpenApplication('{0}',{1})\">{2}</a>",
                this.grid.TaskID,
                record.data.TaskID,
                YZSoft.HttpUtility.HtmlEncode(value));
            //YZSoft.BPM.FormManager.OpenFormApplication('Express\寄件人信息', this.store.getAt(rowIndex).data.TaskID, 'Read', true, {
            //    title: '查看',
            //    width: 830,
            //    height: 505
            //});
        },

        EditRow: function(stamp) {
            YZSoft.BPM.FormManager.OpenFormApplication('Express\寄件人信息', stamp.TaskID, 'Edit', true, {
                title: '修改',
                 width:830,
                height:505,
                listeners: {
                    scope: this,
                    close: function(form) {
                        if (form.dialogResult=='ok') {
                            this.grid.loadSelectId = form.returnValue.key;
                            this.store.reload({ params: { start: this.store.cursor} });
                        }
                    }
                }
            });
        },

        updateStatus: function() {
            this.btnNew.setDisabled(!YZSoft.UIHelper.IsOptEnable(this, this.grid, 'New'));
            this.btnEdit.setDisabled(!YZSoft.UIHelper.IsOptEnable(this, this.grid, 'Edit'));
            this.btnDelete.setDisabled(!YZSoft.UIHelper.IsOptEnable(this, this.grid, 'Delete'));

            var sm = this.grid.getSelectionModel();
            recs = sm.getSelections() || new Array();

            if (YZSoft.UIHelper.IsOptEnable(this, this.grid, 'Edit')) {
                this.btnEdit.setDisabled(recs.length != 1);
            }
            if (YZSoft.UIHelper.IsOptEnable(this, this.grid, 'Delete')) {
                this.btnDelete.setDisabled(recs.length == 0);
            }
            
        }
    });
