import initialState from "../../../../../initialState";



export default (state = initialState.editPolicy, action) => {
    switch (action.type) {

        case "POLICY_SELECT":
            return {
                ...state,
                // editPolicy:
                // {
                //     ...state.editPolicy,
                [action.name]:
                {
                    value: action.val,
                },
                // }
            }

        case "POLICY_LOAD_START":
            return {
                ...state,
                // editPolicy:
                // {
                loading: true,
                id: {
                    value: '',
                },
                title: {
                    value: '',
                },
                description: {
                    value: '',
                }
                // }
            }

        case "POLICY_LOAD_SUCCESS":
            return {
                ...state,
                // editPolicy:
                // {
                //     ...state.editPolicy,
                loading: false,
                ...Object.fromEntries(Object.entries(action.data).map(x => ([
                    x[0],
                    {
                        value: x[1],
                    }
                ]))),
                // }
            }

        default:
            return state
    }
}