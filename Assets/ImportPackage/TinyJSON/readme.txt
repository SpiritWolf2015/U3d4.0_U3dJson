JSON可以用tinyjson，读写都实现了。。。支持json里面注释

没用过SIMPLEJSON，和MiniJSON、MyJSON比较过，比他们都快
LITJSON我测试效率比MiniJSON慢很多的，所以实现后没比较过


问一下 你的 json 解析返回对象的函数在哪？
PowerGridModel model = JsonConvert.DeserializeObject<PowerGridModel>(www.text);

Parser parser = new Parser();
Node node = parser.Load(xxx);
node就是返回的对象了

node 可以 as 么？
可以强转，不能as 
(List<Node>)node
(Dictionary<string, Node>)node
支持这2种，只能转基础类型，和List<Node>，Dictionary<string, Node>


也就是说我的数据格式 是不支持的 对么？
对，你需要转成List<Node>或Dictionary<string, Node>，根据你的数据是[开头还是{开头，然后遍历序列化