import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "ACCOUNTSTATUS_LOADALL_SUCCESS":
            return {
                ...state,
                accountStatuses:
                {
                    loading: false,
                    value: action.data,
                }
            }

        case "ACCOUNTSTATUS_LOADALL_START":
            return {
                ...state,
                accountStatuses:
                {
                    loading: true,
                    value:
                        [
                            {
                                id: {
                                    value: '',
                                },
                                title: {
                                    value: '',
                                },
                                description: {
                                    value: '',
                                },
                            },
                        ],
                }
            }

        case "ACCOUNTSTATUS_REMOVE_START":
            return {
                ...state,
                accountStatuses:
                {
                    ...state.accountStatuses,
                    loading: true,
                }
            }

        default:
            return state
    }
}