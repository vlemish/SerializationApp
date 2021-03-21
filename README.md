<h2>Overview</h2>
<p>The SerializationApp provides the way to serialize and deserialize the structure of directory in a really easy way. After deserializtion app returns the structure of deserialized directory to a console in a pretty tree form.</p> 
<h2>Startup arguments</h2>
<div>
<h3>Serialize</h3>
<p>To serialize a structure you need to provide four args. The args are:</p>
<ul>
<li>Type - the type of serializer, it can be either binary or xml.</li>
<li>Operation - the operation to perform. It can be either ser[serialize] or des[deserialize].</li>
<li>File to which save the structure - the file to store serialized structure.</li>
<li>The path to the directory you want to serialize.</li>
</ul>
<p><b>All of these args are required! Without them you will have an error!</b></p>
<h3>Deserialize</h3>
<p>To deserialize a structure you need to provide three args - a type, an operation (des) and the file that contains serialized structure</p>
</div>
<h2>Example of use</h2>
<p>Look at the gif below, I guess the example is straightforward and doesn't need any additional comments.</p>
<img src="https://github.com/vlemish/SerializationApp/blob/master/imgs/serialization-deserialization-example.gif" alt="Example of serialization and deserialization" width="959" height="480">