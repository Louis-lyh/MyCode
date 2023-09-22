using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LouisCode.DataStructure
{
    /// <summary>
    ///  树节点
    /// </summary>
    class TreeNode
    {
        public TreeNode(int data)
        {
            Data = data;
        }
        // 数据
        public int Data;
        // 左儿子
        public TreeNode Left;
        // 右儿子
        public TreeNode Right;
        // 高
        public int Height;
    }
    /// <summary>
    ///  二叉搜索树
    /// </summary>
    class BinarySearchTree
    {
        // 根节点
        private TreeNode RootNode;
        // 判断是否为空
        public bool IsEmpty()
        {
            return RootNode == null;
        }
        
        /// <summary>
        /// 获取高度
        /// </summary>
        public int Height=>GetHeight(RootNode);
        private int GetHeight(TreeNode node)
        {
            if(node == null)
                return 0;

            var leftHeight = GetHeight(node.Left) + 1;
            var rightHeight = GetHeight(node.Right) + 1;

            return Mathf.Max(leftHeight,rightHeight);
        }
        
        /// <summary>
        /// 查找元素返回其所在节点的地址  
        /// </summary>
        /// <param name="data">元素</param>
        /// <returns></returns>
        public TreeNode Find(int data)
        {
            var temp = RootNode;
            while(temp != null)
            {
                // 找到
                if(temp.Data == data)
                    return temp;
                // 在坐标
                if(temp.Data > data)
                    temp = temp.Left;
                // 在右边
                else
                    temp = temp.Right;
            }
            return null;
        }    
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="data"></param>
        public void Insert(int data)
        {
            // 新建节点
            TreeNode newNode = new TreeNode(data);

            if (RootNode == null)
                RootNode = newNode;
            else
            {
                var temp = RootNode;
                while (temp != null)
                {
                    // 左边
                    if (newNode.Data <= temp.Data)
                    {
                        // 放入左边
                        if (temp.Left == null)
                        {
                            temp.Left = newNode;
                            // 更新高度
                            temp.Height = GetHeight(temp);
                            return;
                        }
                        else // 往左边走
                        {
                            temp = temp.Left;
                            continue;
                        }
                    }
                    // 右边
                    else
                    {
                        // 放入右边
                        if (temp.Right == null)
                        {
                            temp.Right = newNode;
                            // 更新高度
                            temp.Height = GetHeight(temp);
                            return;
                        }
                        else// 往右边走
                        {
                            temp = temp.Right;
                            continue;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 递归插入
        /// </summary>
        /// <param name="data">插入的元素</param>
        public void RecursiveInset(int data)
        {
            RootNode = RecursiveInset(data,RootNode);
        }
        private TreeNode RecursiveInset(int data,TreeNode node)
        {
            if(node == null)
            {
                TreeNode newNode = new TreeNode(data);
                node = newNode;
                return node;
            }
            else
            {
                if(data <= node.Data)
                {
                    node.Left = RecursiveInset(data,node.Left);
                    /* 如果需要左旋 */
                    if ( GetHeight(node.Left)-GetHeight(node.Right) == 2 )
                        if ( data < node.Left.Data ) 
                            node = SingleLeftRotation(node);      /* 左单旋 */
                        else 
                            node = DoubleLeftRightRotation(node); /* 左-右双旋 */
                }
                else
                {
                    node.Right = RecursiveInset(data,node.Right);
                    /* 如果需要右旋 */
                    if ( GetHeight(node.Left)-GetHeight(node.Right) == -2 )
                        if ( data > node.Right.Data ) 
                            node = SingleRightRotation(node);     /* 右单旋 */
                        else 
                           node = DoubleRightLeftRotation(node); /* 右-左双旋 */
                }
                /* 别忘了更新树高 */
                node.Height = GetHeight(node);

                return node;
            }
        }
        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="data">元素</param>
        public void Delete(int data)
        {
            RootNode = Delete(data,RootNode);
        }
        private TreeNode Delete(int data,TreeNode node)
        {
            if(node == null)
            {
                Debug.LogError("没有该元素 "+data);
                return null;
            }
            else
            {
                // 找到
                if(node.Data == data)
                {
                    // 删除的节点两个子节点都在
                    if(node.Left != null && node.Right != null)
                    {   
                        // 找到右边最大
                        var temp = FindMax(node.Left);
                        // 替换
                        node.Data = temp.Data;
                        // 删除
                        node.Left = Delete(node.Data,node.Left);                    
                    }
                    // 没有
                    else if(node.Left != null)
                        node = node.Left;
                    else if(node.Right != null)
                        node = node.Right;

                    return node;
                }
                // 小于节点往左边走
                else if(data < node.Data)
                {
                    node.Left = Delete(data,node.Left);
                }
                // 大于节点往右边走
                else if(data > node.Data)
                {
                    node.Right = Delete(data,node.Right);
                }

                return node;
            }
        }
        /// <summary>
        /// 查找最小元素
        /// </summary>
        /// <returns></returns>
        public TreeNode FindMin()
        {
            return FindMin(RootNode);
        }
        private TreeNode FindMin(TreeNode node)
        {
            // 为空退出
            if(IsEmpty())
                return null;

            var temp = node;
            
            // 找到最左边的节点
            while(temp != null)
            {
                // 左边为空 返回
                if(temp.Left == null)
                    return temp;
                else // 左边不为空继续循环
                    temp = temp.Left;
            }

            return null;
        }
        /// <summary>
        /// 查找最大元素
        /// </summary>
        /// <returns></returns>
        public TreeNode FindMax()
        {
            return FindMax(RootNode);
        }
        private TreeNode FindMax(TreeNode node)
        {
            // 为空退出
            if(IsEmpty())
                return null;
            
            var temp = node;

            // 找到最右边的节点
            while(temp != null)
            {
                // 右边为空 返回
                if(temp.Right == null)
                    return temp;
                else // 右边不为空继续循环
                    temp = temp.Right;
            }
            return null;
        }
        /// <summary>
        /// 先序遍历
        /// </summary>
        public void PreOrderTraversal()
        {
            Debug.Log("先序遍历:");
            RecursiveTraversal(RootNode, 0);
        }
        /// <summary>
        /// 中序遍历
        /// </summary>
        public void InOrderTraversal()
        {
            Debug.Log("中序遍历:");
            RecursiveTraversal(RootNode, 1);
        }
        /// <summary>
        /// 后序遍历
        /// </summary>
        public void PostOrderTraversal()
        {
            Debug.Log("后序遍历:");
            RecursiveTraversal(RootNode, 2);
        }
        /// <summary>
        ///  层次遍历
        /// </summary>
        public void LevelOrderTraversal()
        {
            Debug.Log("层次遍历:");
            // 为空退出
            if(IsEmpty())
                return;

            // 创建队列
            Queue<TreeNode> queue = new Queue<TreeNode>(10);
            queue.AddQueue(RootNode);

            while(!queue.IsEmptry())
            {
                // 拿取一个
                var temp = queue.Delete();
                // 打印
                Debug.Log(""+temp.Data);
                // 左边放入队列
                if(temp.Left != null)
                    queue.AddQueue(temp.Left);
                // 右边放入队列
                if(temp.Right != null)
                    queue.AddQueue(temp.Right);
            }

        }
        /// <summary>
        ///  递归遍历
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="orderType"></param>
        private void RecursiveTraversal(TreeNode treeNode,int orderType)
        {
            if (treeNode != null) 
            {
                // 先序遍历输出
                if(orderType == 0)
                    Debug.Log(treeNode.Data + " ");
               
                // 递归左子树
                RecursiveTraversal(treeNode.Left,orderType);
                
                // 中序遍历输出
                if(orderType == 1)
                    Debug.Log(treeNode.Data + " ");
                
                // 递归右子树
                RecursiveTraversal(treeNode.Right,orderType);
                
                // 后序遍历输出
                if(orderType == 2)
                    Debug.Log(treeNode.Data + " ");
            }
        }

        /// <summary>
        ///  左单旋
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        private TreeNode SingleLeftRotation(TreeNode A)
        {
            /* 注意：A必须有一个左子结点B */
            /* 将A与B做左单旋，更新A与B的高度，返回新的根结点B */     

            TreeNode B = A.Left;
            A.Left = B.Right;
            B.Right = A;
            A.Height = Mathf.Max( GetHeight(A.Left), GetHeight(A.Right) ) + 1;
            B.Height = Mathf.Max( GetHeight(B.Left), A.Height ) + 1;
        
            return B;
        }
        /// <summary>
        ///  右单旋
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        private TreeNode SingleRightRotation(TreeNode A )
        {
            /* 注意：A必须有一个右子结点B */
            /* 将A与B做左单旋，更新A与B的高度，返回新的根结点B */  

            TreeNode B = A.Right;
            A.Right = B.Left;
            B.Left = A;

            A.Height = Mathf.Max( GetHeight(A.Left), GetHeight(A.Right) ) + 1;
            B.Height = Mathf.Max( GetHeight(B.Left), A.Height ) + 1;
        
            return B;
        }
        /// <summary>
        /// 左右双旋
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        private TreeNode DoubleLeftRightRotation ( TreeNode A )
        { 
            /* 注意：A必须有一个左子结点B，且B必须有一个右子结点C */
            /* 将A、B与C做两次单旋，返回新的根结点C */
            
            /* 将B与C做右单旋，C被返回 */
            A.Left = SingleRightRotation(A.Left);
            /* 将A与C做左单旋，C被返回 */
            return SingleLeftRotation(A);
        }
        /// <summary>
        /// 右左双旋
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        private TreeNode DoubleRightLeftRotation(TreeNode A) 
        {
             /* 注意：A必须有一个右子结点B，且B必须有一个左子结点C */
            /* 将A、B与C做两次单旋，返回新的根结点C */
            
            /* 将B与C做左单旋，C被返回 */
            A.Right = SingleLeftRotation(A.Right);
            /* 将A与C做右单旋，C被返回 */
            return SingleRightRotation(A);
        }

        public override string ToString()
        {
            var maxHeight = Height;
            string str = "";
            
             // 为空退出
            if(IsEmpty())
                return str;
            
            // 当前高度
            int curHeight = 0;
            // 当前层节点索引
            int curIndex = 0;

            // 创建队列
            Queue<TreeNode> queue = new Queue<TreeNode>(10);
            queue.AddQueue(RootNode);

            while(!queue.IsEmptry())
            {
                // 拿取一个
                var temp = queue.Delete();
                // 当前高度
                var tempHeight = temp.Height;
                if(temp.Data != int.MinValue)
                    tempHeight = GetHeight(temp);
                // 层不同 换行，缩进
                if(tempHeight != curHeight)
                {
                    str += "\n";
                    // 更新当前高度
                    curHeight = tempHeight;
                    // 重新计算索引
                    curIndex = 0;
                }
                // 索引加一
                curIndex++;
                // 输出值
                for(int i = 0; i < Mathf.Pow(2,tempHeight - 1) - 1; i++)
                {
                    str += " ";
                }
                if(temp.Data != int.MinValue)
                    str += $"{temp.Data:D2}";
                else
                    str += "__";
                for(int i = 0; i < Mathf.Pow(2,tempHeight - 1) - 1; i++)
                    str += " ";
                
                
                // 左边放入队列
                if(temp.Left != null)
                    queue.AddQueue(temp.Left);
                else if(tempHeight - 1 > 0) // 为空放入一个空的站位
                {
                    TreeNode nullNode = new TreeNode(int.MinValue);
                    nullNode.Height = tempHeight - 1;
                    queue.AddQueue(nullNode);
                }
                // 右边放入队列
                if(temp.Right != null)
                    queue.AddQueue(temp.Right);
                else if(tempHeight - 1 > 0) // 为空放入一个空的站位
                {
                    TreeNode nullNode = new TreeNode(int.MinValue);
                    nullNode.Height = tempHeight - 1;
                    queue.AddQueue(nullNode);
                }
            }
            return str;
        }

    }
}


