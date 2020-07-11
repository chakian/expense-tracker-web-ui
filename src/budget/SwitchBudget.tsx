import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Dispatch, AnyAction, bindActionCreators } from 'redux';
import { Table, Space } from 'antd';

import { AppState } from '../_store/rootReducer';

const columns = [
    {
        title: 'Bütçe Adı',
        dataIndex: 'budgetName',
        key: 'budgetName',
    },
    {
        title: 'Son Güncelleme',
        dataIndex: 'lastUpdate',
        key: 'lastUpdate',
    },
    {
        title: 'Değişim',
        key: 'action',
        render: (text, record) =>
            (
                <Space size="middle">
                    {
                        record.isDefault == true ?
                            "Aktif" :
                            <a>Aktifleştir</a>
                    }
                </Space>
            ),
    },
];

const data = [
    {
        key: '1',
        budgetName: 'Deneme',
        lastUpdate: '2020-01-01',
        isDefault: true,
    },
    {
        key: '2',
        budgetName: 'Deneme 2',
        lastUpdate: '2020-07-01',
        isDefault: false,
    },
];

class SwitchBudget extends Component<ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>, {}> {
    componentDidMount() {
    }

    render() {
        return (
            <div>
                <header>Bütçe Değiştir</header>
                <Table columns={columns} dataSource={data} />
            </div>
        );
    }
}

const mapStateToProps = (state: AppState) => ({
    user: state.user
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
