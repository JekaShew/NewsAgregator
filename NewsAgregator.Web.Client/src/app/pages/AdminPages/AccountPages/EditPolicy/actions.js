export const add = (newPolicy) => ({
    type: 'POLICY_ADD',
    remote: {
        url: '/api/policy/add',
        type: 'post',
        body: newPolicy,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedPolicy) => ({
    type: 'POLICY_SAVE',
    remote: {
        url: '/api/policy/update',
        type: 'post',
        body: changedPolicy,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'POLICY_SELECT',
    name: name,
    val: value,
})


export const loadData = (id) => ({
    type: 'POLICY_LOAD',
    remote: {
        url: '/api/policy/takebyid?id=' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'POLICY_LOAD_SUCCESS',

    data:
    {
        title: {
            value: '',
        },
        description: {
            value: '',
        }
    }
})