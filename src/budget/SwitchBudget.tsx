import React, { Component, useState } from 'react';
import { connect } from 'react-redux';
import { Dispatch, AnyAction, bindActionCreators } from 'redux';
import { Table, Space, Button } from 'antd';

import { AppState } from '../_store/rootReducer';
import { getBudgetList } from '../_services/budgetService';

class budgetData {
    key: number;
    budgetName: string;
    isDefault: boolean;
}

class SwitchBudget extends Component<ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>, {}> {
    state = {
        isLoading: true,
        columns: [
            {
                title: 'Bütçe Adı',
                dataIndex: 'budgetName',
                key: 'budgetName',
            },
            // {
            //     title: 'Son Güncelleme',
            //     dataIndex: 'lastUpdate',
            //     key: 'lastUpdate',
            // },
            {
                title: 'Değişim',
                key: 'action',
                render: (text, record) =>
                    (
                        <Space size="middle">
                            {
                                record.isDefault == true ?
                                    "Aktif" :
                                    "Aktifleştir ?"
                                    // <a>Aktifleştir</a>
                            }
                        </Space>
                    ),
            },
        ],
        tableData: []
    };

    componentDidMount() {
        this.onGetList();
    }

    onGetList = () => {
        const { user } = this.props;
        let prm = getBudgetList(user.token);
        prm.then((list) => {
            this.setState({ isLoading: false });

            const { defaultBudgetId } = user;

            let dataArr = Array<budgetData>();

            for (let item of list.list) {
                let dataPart = {
                    key: item.budgetId,
                    budgetName: item.budgetName,
                    isDefault: false,
                };
                if (item.budgetId === defaultBudgetId) {
                    dataPart.isDefault = true;
                }
                dataArr.push(dataPart);
            }

            this.setState({ tableData: dataArr });
        }, (error) => {
            alert(error);
        });
    }

    render() {
        const { isLoading, columns, tableData } = this.state;

        return (
            <div>
                <header>Bütçe Değiştir <Button onClick={this.onGetList}>Yenile</Button> </header>
                <Table columns={columns} dataSource={tableData} loading={isLoading} />
            </div>
        );
    }
}

const mapStateToProps = (state: AppState) => ({
    user: state.user,
    budget: state.budget
});

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) =>
    bindActionCreators(
        {
        },
        dispatch);

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(SwitchBudget);
